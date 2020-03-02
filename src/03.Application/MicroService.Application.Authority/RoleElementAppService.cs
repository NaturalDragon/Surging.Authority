
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
 namespace MicroService.Application.Authority
 {
	/// <summary>
	/// RoleElement -applaction实现
	/// </summary>
	public class RoleElementAppService:IRoleElementAppService
	{
     
        private readonly IRoleElementRespository _roleElementRespository;
        private readonly IMapper _mapper;
        public RoleElementAppService(IRoleElementRespository roleElementRespository,
          IMapper mapper)
        {
            _roleElementRespository = roleElementRespository;
            _mapper = mapper;
        }
    
        /// <summary>
          /// 新增
          /// </summary>
          /// <param name="roleElementRequestDto"></param>
          /// <returns></returns>
        public async Task<bool> CreateAsync(RoleElementRequestDto roleElementRequestDto)
        {
         
            var roleElement = roleElementRequestDto.MapToCreateEntity<RoleElementRequestDto, RoleElement>();
            await RoleElementValidatorsFilter.DoValidationAsync(_roleElementRespository,roleElement, ValidatorTypeConstants.Create);
            return await _roleElementRespository.InsertAsync(roleElement);  
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="roleElementRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchCreateAsync(IList<RoleElementRequestDto> roleElementRequestDtos)
        {
            var entities =roleElementRequestDtos.MapToCreateEntities<RoleElementRequestDto, RoleElement>();
            await RoleElementValidatorsFilter.DoValidationAsync(_roleElementRespository,entities, ValidatorTypeConstants.Create);
            await _roleElementRespository.BatchInsertAsync(entities);
            return true;
         
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="roleElementRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchRemoveAsync(IList<RoleElementRequestDto> roleElementRequestDtos)
        {
            var roleIds = roleElementRequestDtos.Select(m => m.RoleId).ToList();
            var elementIds = roleElementRequestDtos.Select(m => m.ElementId).ToList();
            var roleElements = await _roleElementRespository.EntitiesByExpressionAsync(e => roleIds.Contains(e.RoleId)
            &&elementIds.Contains(e.ElementId)&&e.IsDelete==false);
            var entities = roleElementRequestDtos.MapToModifyEntities<RoleElementRequestDto, RoleElement>(roleElements.ToList());
            await RoleElementValidatorsFilter.DoValidationAsync(_roleElementRespository, entities, ValidatorTypeConstants.Create);
            await _roleElementRespository.BatchUpdateAsync(entities);
            return true;
        }
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="roleElementPageRequestDto"></param>
        /// <returns></returns>

        public async Task<PageData> GetPageListAsync( RoleElementPageRequestDto roleElementPageRequestDto)
        {
            var pageData = new PageData(roleElementPageRequestDto.PageIndex, roleElementPageRequestDto.PageSize);
            var list = await _roleElementRespository.WherePaged(pageData, e => e.IsDelete == false,
                e => e.CreateDate, false);
			 pageData.Data = list.MapToList<RoleElement, RoleElementQueryDto>().ToList();
            return pageData;

        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        public async Task<RoleElementQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest)
        {
            var entity = await _roleElementRespository.FirstOrDefaultAsync(e => e.Id == entityQueryRequest.Id);
            if (entity != null)
            {
                return entity.MapEntity<RoleElement, RoleElementQueryDto>();
            }
            return null;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="roleElementRequestDto"></param>
        /// <returns></returns>
        public async Task<bool> ModifyAsync(RoleElementRequestDto roleElementRequestDto)
        {
            var roleElement = await _roleElementRespository.FirstOrDefaultAsync(e => e.Id == roleElementRequestDto.Id);
            var entity = roleElementRequestDto.MapToModifyEntity<RoleElementRequestDto, RoleElement>(roleElement);
            await RoleElementValidatorsFilter.DoValidationAsync(_roleElementRespository,entity, ValidatorTypeConstants.Modify);
            return await _roleElementRespository.UpdateAsync(entity); 
        }

		/// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="roleElementRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchModifyAsync(IList<RoleElementRequestDto> roleElementRequestDtos)
        {
            var ids = roleElementRequestDtos.Select(m => m.Id).ToList();
            var roleElements = await _roleElementRespository.EntitiesByExpressionAsync(e => ids.Contains(e.Id));
            var entities = roleElementRequestDtos.MapToModifyEntities<RoleElementRequestDto,RoleElement>(roleElements.ToList());
            await RoleElementValidatorsFilter.DoValidationAsync(_roleElementRespository, entities, ValidatorTypeConstants.Create);
            await _roleElementRespository.BatchUpdateAsync(entities);
            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(EntityRequest entityRequest)
        {
            return await _roleElementRespository.UpdateAsync(entityRequest.Ids.ToArray(), async (e) =>
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
        /// 根据roleId获取模块元素
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public async Task<IList<RoleElementQueryDto>> GetElementByRoleIdsAsync(IList<string> roleIds)
        {
            var list = await _roleElementRespository.EntitiesByExpressionAsync(r => roleIds.Contains(r.RoleId) && r.IsDelete == false,
                r => new RoleElementQueryDto { ElementId = r.ElementId });
            return list.ToList();
        }

        /// <summary>
        /// 根据模块元素Id获取主键
        /// </summary>
        /// <param name="moduleIds"></param>
        /// <returns></returns>
        public async Task<IList<string>> GetIdsByModuleElementIdsAsync(IList<string> moduleElementIds)
        {
            var list = await _roleElementRespository.EntitiesByExpressionAsync(o =>
            moduleElementIds.Contains(o.ElementId) && o.IsDelete == false,
                 o => new RoleElementQueryDto { Id = o.Id });
            return list.Select(o => o.Id).ToList();
        }
        /// 根据roleIds获取主键
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public async Task<IList<string>> GetIdsByRoleIdsAsync(IList<string> roleIds)
        {
            var list = await _roleElementRespository.EntitiesByExpressionAsync(o =>
            roleIds.Contains(o.RoleId) && o.IsDelete == false,
                 o => new RoleElementQueryDto { Id = o.Id });
            return list.Select(o => o.Id).ToList();
        }
    }
}