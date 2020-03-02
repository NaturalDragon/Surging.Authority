
using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MicroService.Data.Ext;
using MicroService.IApplication.Authority;
using MicroService.IRespository.Authority;
using MicroService.Entity.Authority;
using MicroService.IApplication.Authority.Dto;
using MicroService.Core.Data;
using MicroService.Data.Validation;
using MicroService.Data.Common;
using MicroService.Common.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicroService.Application.Authority.ValidatorsFilters;
using MicroService.Common.Core.Enums;

namespace MicroService.Application.Authority
 {
	/// <summary>
	/// ModuleElement -applaction实现
	/// </summary>
	public class ModuleElementAppService:IModuleElementAppService
	{
     
        private readonly IModuleElementRespository _moduleElementRespository;
        private readonly IMapper _mapper;
        public ModuleElementAppService(IModuleElementRespository moduleElementRespository,
          IMapper mapper)
        {
            _moduleElementRespository = moduleElementRespository;
            _mapper = mapper;
        }
    
        /// <summary>
          /// 新增
          /// </summary>
          /// <param name="moduleElementRequestDto"></param>
          /// <returns></returns>
        public async Task<bool> CreateAsync(ModuleElementRequestDto moduleElementRequestDto)
        {
         
            var moduleElement = moduleElementRequestDto.MapToCreateEntity<ModuleElementRequestDto, ModuleElement>();
            await ModuleElementValidatorsFilter.DoValidationAsync(_moduleElementRespository,moduleElement, ValidatorTypeConstants.Create);
            return await _moduleElementRespository.InsertAsync(moduleElement);  
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="moduleElementRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchCreateAsync(IList<ModuleElementRequestDto> moduleElementRequestDtos)
        {
            var entities =moduleElementRequestDtos.MapToCreateEntities<ModuleElementRequestDto, ModuleElement>();
            await ModuleElementValidatorsFilter.DoValidationAsync(_moduleElementRespository,entities, ValidatorTypeConstants.Create);
            await _moduleElementRespository.BatchInsertAsync(entities);
            return true;
         
        }

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="moduleElementPageRequestDto"></param>
        /// <returns></returns>

        public async Task<PageData> GetPageListAsync( ModuleElementPageRequestDto moduleElementPageRequestDto)
        {
            var pageData = new PageData(moduleElementPageRequestDto.PageIndex, moduleElementPageRequestDto.PageSize);
            var list = await _moduleElementRespository.WherePaged(pageData, e => e.IsDelete == false,
                e => e.CreateDate, false);
			 pageData.Data = list.MapToList<ModuleElement, ModuleElementQueryDto>().ToList();
            return pageData;

        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        public async Task<ModuleElementQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest)
        {
            var entity = await _moduleElementRespository.FirstOrDefaultAsync(e => e.Id == entityQueryRequest.Id);
            if (entity != null)
            {
                return entity.MapEntity<ModuleElement, ModuleElementQueryDto>();
            }
            return null;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="moduleElementRequestDto"></param>
        /// <returns></returns>
        public async Task<bool> ModifyAsync(ModuleElementRequestDto moduleElementRequestDto)
        {
            var moduleElement = await _moduleElementRespository.FirstOrDefaultAsync(e => e.Id == moduleElementRequestDto.Id);
            var entity = moduleElementRequestDto.MapToModifyEntity<ModuleElementRequestDto, ModuleElement>(moduleElement);
            await ModuleElementValidatorsFilter.DoValidationAsync(_moduleElementRespository,entity, ValidatorTypeConstants.Modify);
            return await _moduleElementRespository.UpdateAsync(entity); 
        }

		/// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="moduleElementRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchModifyAsync(IList<ModuleElementRequestDto> moduleElementRequestDtos)
        {
            var ids = moduleElementRequestDtos.Select(m => m.Id).ToList();
            var moduleElements = await _moduleElementRespository.EntitiesByExpressionAsync(e => ids.Contains(e.Id));
            var entities = moduleElementRequestDtos.MapToModifyEntities<ModuleElementRequestDto,ModuleElement>(moduleElements.ToList());
            await ModuleElementValidatorsFilter.DoValidationAsync(_moduleElementRespository, entities, ValidatorTypeConstants.Create);
            await _moduleElementRespository.BatchUpdateAsync(entities);
            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(EntityRequest entityRequest)
        {
            return await _moduleElementRespository.UpdateAsync(entityRequest.Ids.ToArray(), async (e) =>
            {
                await Task.Run(() =>
               {
                   e.IsDelete = true;
                   e.ModifyUserId = entityRequest.ModifyUserId;
                   e.ModifyUserName = entityRequest.ModifyUserName;
                   e.ModifyDate = entityRequest.ModifyDate;
               });
            });
         
        }

        /// <summary>
        /// 根据模块Id获取其元素集合
        /// </summary>
        /// <param name="moduleIds"></param>
        /// <returns></returns>
        public async Task<IList<ModuleElementQueryDto>> GetElementByModuleIds(IList<string> moduleIds)
        {
            var moduleElements =await _moduleElementRespository.EntitiesByExpressionAsync(e => moduleIds.Contains(e.ModuleId)&&e.IsDelete==false,
                e => new ModuleElementQueryDto()
                {
                    Id = e.Id,
                    Name = e.Name,
                    RoutePath = e.RoutePath,
                    sortIndex = e.sortIndex,
                    ModuleId = e.ModuleId,
                    OperationStatus=OperationModel.Modify
                },e=>e.sortIndex,true);
            return moduleElements.ToList();
        }
        /// <summary>
        /// 根据ids获取其元素集合
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IList<ModuleElementQueryDto>> GetElementByIds(IList<string> ids)
        {
            var moduleElements = await _moduleElementRespository.EntitiesByExpressionAsync(e => ids.Contains(e.Id) && e.IsDelete == false,
              e => new ModuleElementQueryDto()
              {
                  Id = e.Id,
                  RoutePath = e.RoutePath,
              });
            return moduleElements.ToList();
        }
        /// <summary>
        /// 根据模块Id获取主键
        /// </summary>
        /// <param name="moduleIds"></param>
        /// <returns></returns>
        public async Task<IList<string>> GetIdsByModuleIdsAsync(IList<string> moduleIds)
        {
            var list = await _moduleElementRespository.EntitiesByExpressionAsync(o =>
            moduleIds.Contains(o.ModuleId) && o.IsDelete == false,
                 o => new ModuleElementQueryDto { Id = o.Id });
            return list.Select(o => o.Id).ToList();
        }
    }
}