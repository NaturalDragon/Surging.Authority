
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
	/// Module -applaction实现
	/// </summary>
	public class ModuleAppService:IModuleAppService
	{
     
        private readonly IModuleRespository _moduleRespository;
        private readonly IMapper _mapper;
        public ModuleAppService(IModuleRespository moduleRespository,
          IMapper mapper)
        {
            _moduleRespository = moduleRespository;
            _mapper = mapper;
        }
    
        /// <summary>
          /// 新增
          /// </summary>
          /// <param name="moduleRequestDto"></param>
          /// <returns></returns>
        public async Task<bool> CreateAsync(ModuleRequestDto moduleRequestDto)
        {
         
            var module = moduleRequestDto.MapToCreateEntity<ModuleRequestDto, Module>();
            await ModuleValidatorsFilter.DoValidationAsync(_moduleRespository,module, ValidatorTypeConstants.Create);
            return await _moduleRespository.InsertAsync(module);  
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="moduleRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchCreateAsync(IList<ModuleRequestDto> moduleRequestDtos)
        {
            var entities =moduleRequestDtos.MapToCreateEntities<ModuleRequestDto, Module>();
            await ModuleValidatorsFilter.DoValidationAsync(_moduleRespository,entities, ValidatorTypeConstants.Create);
            await _moduleRespository.BatchInsertAsync(entities);
            return true;
         
        }

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="modulePageRequestDto"></param>
        /// <returns></returns>

        public async Task<PageData> GetPageListAsync( ModulePageRequestDto modulePageRequestDto)
        {
            var pageData = new PageData(modulePageRequestDto.PageIndex, modulePageRequestDto.PageSize);

            var list = await _moduleRespository.WherePaged(pageData, e => e.IsDelete == false,
                o => new ModuleQueryDto { Id = o.Id, CreateDate = o.CreateDate, Name = o.Name, Url = o.Url, IsEnabled = o.IsEnabled },
                    o => new Dictionary<object, QueryOrderBy>() {
                        { o.IsEnabled, QueryOrderBy.Asc },
                        { o.CreateDate, QueryOrderBy.Desc } }
                );
            pageData.Data = list;// list.MapToList<Module, ModuleQueryDto>().ToList();
            return pageData;

        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        public async Task<ModuleQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest)
        {
            var entity = await _moduleRespository.FirstOrDefaultAsync(e => e.Id == entityQueryRequest.Id);
            if (entity != null)
            {
                return entity.MapEntity<Module, ModuleQueryDto>();
            }
            return null;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="moduleRequestDto"></param>
        /// <returns></returns>
        public async Task<bool> ModifyAsync(ModuleRequestDto moduleRequestDto)
        {
            var module = await _moduleRespository.FirstOrDefaultAsync(e => e.Id == moduleRequestDto.Id);
            var entity = moduleRequestDto.MapToModifyEntity<ModuleRequestDto, Module>(module);
            await ModuleValidatorsFilter.DoValidationAsync(_moduleRespository,entity, ValidatorTypeConstants.Modify);
            return await _moduleRespository.UpdateAsync(entity); 
        }

		/// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="moduleRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchModifyAsync(IList<ModuleRequestDto> moduleRequestDtos)
        {
            var ids = moduleRequestDtos.Select(m => m.Id).ToList();
            var modules = await _moduleRespository.EntitiesByExpressionAsync(e => ids.Contains(e.Id));
            var entities = moduleRequestDtos.MapToModifyEntities<ModuleRequestDto,Module>(modules.ToList());
            await ModuleValidatorsFilter.DoValidationAsync(_moduleRespository, entities, ValidatorTypeConstants.Create);
            await _moduleRespository.BatchUpdateAsync(entities);
            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(EntityRequest entityRequest)
        {
            return await _moduleRespository.UpdateAsync(entityRequest.Ids.ToArray(), async (e) =>
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
        /// 获取全部
        /// </summary>
        /// <returns></returns>
        public async Task<List<ModuleQueryDto>> GetAll()
        {
            var list = await _moduleRespository.EntitiesByExpressionAsync(e => e.IsDelete == false,
                   e => new ModuleQueryDto { Name = e.Name, Url = e.Url, Id = e.Id });
            return list.ToList();
        }
        /// <summary>
        /// 根据ids获取模块
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
       public async Task<List<ModuleQueryDto>> GetModuleByIdsAsnyc(IList<string> ids)
        {
            var list = await _moduleRespository.EntitiesByExpressionAsync(e => ids .Contains(e.Id)&& e.IsDelete == false,
                  e => new ModuleQueryDto {  Url = e.Url, Id = e.Id });
            return list.ToList();
        }
    }
}