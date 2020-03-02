
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
	/// OrganizationMenu -applaction实现
	/// </summary>
	public class OrganizationMenuAppService:IOrganizationMenuAppService
	{
     
        private readonly IOrganizationMenuRespository _organizationMenuRespository;
        private readonly IMapper _mapper;
        public OrganizationMenuAppService(IOrganizationMenuRespository organizationMenuRespository,
          IMapper mapper)
        {
            _organizationMenuRespository = organizationMenuRespository;
            _mapper = mapper;
        }
    
        /// <summary>
          /// 新增
          /// </summary>
          /// <param name="organizationMenuRequestDto"></param>
          /// <returns></returns>
        public async Task<bool> CreateAsync(OrganizationMenuRequestDto organizationMenuRequestDto)
        {
         
            var organizationMenu = organizationMenuRequestDto.MapToCreateEntity<OrganizationMenuRequestDto, OrganizationMenu>();
            await OrganizationMenuValidatorsFilter.DoValidationAsync(_organizationMenuRespository,organizationMenu, ValidatorTypeConstants.Create);
            return await _organizationMenuRespository.InsertAsync(organizationMenu);  
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="organizationMenuRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchCreateAsync(IList<OrganizationMenuRequestDto> organizationMenuRequestDtos)
        {
            var entities =organizationMenuRequestDtos.MapToCreateEntities<OrganizationMenuRequestDto, OrganizationMenu>();
            await OrganizationMenuValidatorsFilter.DoValidationAsync(_organizationMenuRespository,entities, ValidatorTypeConstants.Create);
            await _organizationMenuRespository.BatchInsertAsync(entities);
            return true;
         
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="organizationMenuRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchRemoveAsync(IList<OrganizationMenuRequestDto> organizationMenuRequestDtos)
        {
            var orgIds = organizationMenuRequestDtos.Select(m => m.OrganizationId).ToList();
            var menuIds= organizationMenuRequestDtos.Select(m => m.MenuId).ToList();
            var organizationMenus = await _organizationMenuRespository.EntitiesByExpressionAsync(e => orgIds.Contains(e.OrganizationId)
            &&menuIds.Contains(e.MenuId)&&e.IsDelete==false);
            var entities = organizationMenuRequestDtos.MapToModifyEntities<OrganizationMenuRequestDto, OrganizationMenu>(organizationMenus.ToList());
            await OrganizationMenuValidatorsFilter.DoValidationAsync(_organizationMenuRespository, entities, ValidatorTypeConstants.Create);
            await _organizationMenuRespository.BatchUpdateAsync(entities);
            return true;
        }

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="organizationMenuPageRequestDto"></param>
        /// <returns></returns>

        public async Task<PageData> GetPageListAsync( OrganizationMenuPageRequestDto organizationMenuPageRequestDto)
        {
            var pageData = new PageData(organizationMenuPageRequestDto.PageIndex, organizationMenuPageRequestDto.PageSize);
            var list = await _organizationMenuRespository.WherePaged(pageData, e => e.IsDelete == false,
                e => e.CreateDate, false);
			 pageData.Data = list.MapToList<OrganizationMenu, OrganizationMenuQueryDto>().ToList();
            return pageData;

        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        public async Task<OrganizationMenuQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest)
        {
            var entity = await _organizationMenuRespository.FirstOrDefaultAsync(e => e.Id == entityQueryRequest.Id);
            if (entity != null)
            {
                return entity.MapEntity<OrganizationMenu, OrganizationMenuQueryDto>();
            }
            return null;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="organizationMenuRequestDto"></param>
        /// <returns></returns>
        public async Task<bool> ModifyAsync(OrganizationMenuRequestDto organizationMenuRequestDto)
        {
            var organizationMenu = await _organizationMenuRespository.FirstOrDefaultAsync(e => e.Id == organizationMenuRequestDto.Id);
            var entity = organizationMenuRequestDto.MapToModifyEntity<OrganizationMenuRequestDto, OrganizationMenu>(organizationMenu);
            await OrganizationMenuValidatorsFilter.DoValidationAsync(_organizationMenuRespository,entity, ValidatorTypeConstants.Modify);
            return await _organizationMenuRespository.UpdateAsync(entity); 
        }

		/// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="organizationMenuRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchModifyAsync(IList<OrganizationMenuRequestDto> organizationMenuRequestDtos)
        {
            var ids = organizationMenuRequestDtos.Select(m => m.Id).ToList();
            var organizationMenus = await _organizationMenuRespository.EntitiesByExpressionAsync(e => ids.Contains(e.Id));
            var entities = organizationMenuRequestDtos.MapToModifyEntities<OrganizationMenuRequestDto,OrganizationMenu>(organizationMenus.ToList());
            await OrganizationMenuValidatorsFilter.DoValidationAsync(_organizationMenuRespository, entities, ValidatorTypeConstants.Create);
            await _organizationMenuRespository.BatchUpdateAsync(entities);
            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(EntityRequest entityRequest)
        {
            return await _organizationMenuRespository.UpdateAsync(entityRequest.Ids.ToArray(), async (e) =>
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
        /// 根据机构orgIds获取菜单
        /// </summary>
        /// <param name="orgIds"></param>
        /// <returns></returns>
        public async Task<IList<OrganizationMenuQueryDto>> GetMenuByOrgIdsAsync(IList<string> orgIds)
        {
            var list = await _organizationMenuRespository.EntitiesByExpressionAsync(o => orgIds.Contains(o.OrganizationId) && o.IsDelete == false,
                  o => new OrganizationMenuQueryDto { MenuId = o.MenuId });
            return list.ToList();
        }

        /// <summary>
        /// 根据菜单Id获取主键
        /// </summary>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        public async Task<IList<string>> GetIdsByMenuIdsAsync(IList<string> menuIds)
        {
            var list = await _organizationMenuRespository.EntitiesByExpressionAsync(o =>
            menuIds.Contains(o.MenuId) && o.IsDelete == false,
                 o => new OrganizationMenuQueryDto { Id = o.Id });
            return list.Select(o=>o.Id).ToList();
        }
        /// <summary>
        /// 根据机构organizationIds获取主键
        /// </summary>
        /// <param name="organizationIds"></param>
        /// <returns></returns>
        public async Task<IList<string>> GetIdsByOrganizationIdsAsync(IList<string> organizationIds)
        {
            var list = await _organizationMenuRespository.EntitiesByExpressionAsync(o =>
          organizationIds.Contains(o.OrganizationId) && o.IsDelete == false,
               o => new OrganizationMenuQueryDto { Id = o.Id });
            return list.Select(o => o.Id).ToList();
        }
    }
}