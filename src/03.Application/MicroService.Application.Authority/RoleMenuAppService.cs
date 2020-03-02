
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
	/// RoleMenu -applaction实现
	/// </summary>
	public class RoleMenuAppService:IRoleMenuAppService
	{
     
        private readonly IRoleMenuRespository _roleMenuRespository;
        private readonly IMapper _mapper;
        public RoleMenuAppService(IRoleMenuRespository roleMenuRespository,
          IMapper mapper)
        {
            _roleMenuRespository = roleMenuRespository;
            _mapper = mapper;
        }
    
        /// <summary>
          /// 新增
          /// </summary>
          /// <param name="roleMenuRequestDto"></param>
          /// <returns></returns>
        public async Task<bool> CreateAsync(RoleMenuRequestDto roleMenuRequestDto)
        {
         
            var roleMenu = roleMenuRequestDto.MapToCreateEntity<RoleMenuRequestDto, RoleMenu>();
            await RoleMenuValidatorsFilter.DoValidationAsync(_roleMenuRespository,roleMenu, ValidatorTypeConstants.Create);
            return await _roleMenuRespository.InsertAsync(roleMenu);  
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="roleMenuRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchCreateAsync(IList<RoleMenuRequestDto> roleMenuRequestDtos)
        {
            var entities =roleMenuRequestDtos.MapToCreateEntities<RoleMenuRequestDto, RoleMenu>();
            await RoleMenuValidatorsFilter.DoValidationAsync(_roleMenuRespository,entities, ValidatorTypeConstants.Create);
            await _roleMenuRespository.BatchInsertAsync(entities);
            return true;
         
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="roleMenuRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchRemoveAsync(IList<RoleMenuRequestDto> roleMenuRequestDtos)
        {
            var roleIds = roleMenuRequestDtos.Select(m => m.RoleId).ToList();
            var menuIds = roleMenuRequestDtos.Select(m => m.MenuId).ToList();
            var roleMenus = await _roleMenuRespository.EntitiesByExpressionAsync(e => roleIds.Contains(e.RoleId)
            &&menuIds.Contains(e.MenuId)&&e.IsDelete==false);
            var entities = roleMenuRequestDtos.MapToModifyEntities<RoleMenuRequestDto, RoleMenu>(roleMenus.ToList());
            await RoleMenuValidatorsFilter.DoValidationAsync(_roleMenuRespository, entities, ValidatorTypeConstants.Create);
            await _roleMenuRespository.BatchUpdateAsync(entities);
            return true;
        }

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="roleMenuPageRequestDto"></param>
        /// <returns></returns>

        public async Task<PageData> GetPageListAsync( RoleMenuPageRequestDto roleMenuPageRequestDto)
        {
            var pageData = new PageData(roleMenuPageRequestDto.PageIndex, roleMenuPageRequestDto.PageSize);
            var list = await _roleMenuRespository.WherePaged(pageData, e => e.IsDelete == false,
                e => e.CreateDate, false);
			 pageData.Data = list.MapToList<RoleMenu, RoleMenuQueryDto>().ToList();
            return pageData;

        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        public async Task<RoleMenuQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest)
        {
            var entity = await _roleMenuRespository.FirstOrDefaultAsync(e => e.Id == entityQueryRequest.Id);
            if (entity != null)
            {
                return entity.MapEntity<RoleMenu, RoleMenuQueryDto>();
            }
            return null;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="roleMenuRequestDto"></param>
        /// <returns></returns>
        public async Task<bool> ModifyAsync(RoleMenuRequestDto roleMenuRequestDto)
        {
            var roleMenu = await _roleMenuRespository.FirstOrDefaultAsync(e => e.Id == roleMenuRequestDto.Id);
            var entity = roleMenuRequestDto.MapToModifyEntity<RoleMenuRequestDto, RoleMenu>(roleMenu);
            await RoleMenuValidatorsFilter.DoValidationAsync(_roleMenuRespository,entity, ValidatorTypeConstants.Modify);
            return await _roleMenuRespository.UpdateAsync(entity); 
        }

		/// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="roleMenuRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchModifyAsync(IList<RoleMenuRequestDto> roleMenuRequestDtos)
        {
            var ids = roleMenuRequestDtos.Select(m => m.Id).ToList();
            var roleMenus = await _roleMenuRespository.EntitiesByExpressionAsync(e => ids.Contains(e.Id));
            var entities = roleMenuRequestDtos.MapToModifyEntities<RoleMenuRequestDto,RoleMenu>(roleMenus.ToList());
            await RoleMenuValidatorsFilter.DoValidationAsync(_roleMenuRespository, entities, ValidatorTypeConstants.Create);
            await _roleMenuRespository.BatchUpdateAsync(entities);
            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(EntityRequest entityRequest)
        {
            return await _roleMenuRespository.UpdateAsync(entityRequest.Ids.ToArray(), async (e) =>
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
        /// 根据roleIds获取菜单
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public async Task<IList<RoleMenuQueryDto>> GetMenuByRoleIdsAsync(IList<string> roleIds)
        {
            var list = await _roleMenuRespository.EntitiesByExpressionAsync(r => roleIds.Contains( r.RoleId) &&
              r.IsDelete == false, r => new RoleMenuQueryDto { MenuId = r.MenuId });
            return list.ToList();
        }

        /// <summary>
        /// 根据菜单Id获取主键
        /// </summary>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        public async Task<IList<string>> GetIdsByMenuIdsAsync(IList<string> menuIds)
        {
            var list = await _roleMenuRespository.EntitiesByExpressionAsync(o =>
            menuIds.Contains(o.MenuId) && o.IsDelete == false,
                 o => new RoleMenuQueryDto { Id = o.Id });
            return list.Select(o => o.Id).ToList();
        }
        /// 根据roleIds获取主键
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
       public async Task<IList<string>> GetIdsByRoleIdsAsync(IList<string> roleIds)
        {
            var list = await _roleMenuRespository.EntitiesByExpressionAsync(o =>
            roleIds.Contains(o.RoleId) && o.IsDelete == false,
                 o => new RoleMenuQueryDto { Id = o.Id });
            return list.Select(o => o.Id).ToList();
        }
    }
}