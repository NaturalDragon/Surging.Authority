
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MicroService.Core;
using MicroService.Data.Common;
using MicroService.Data.Validation;
using MicroService.Common.Models;
using MicroService.Entity.Authority;
using MicroService.IApplication.Authority.Dto;

 namespace MicroService.IApplication.Authority
 {
	/// <summary>
	/// OrganizationMenu -applaction实现
	/// </summary>
	public interface IOrganizationMenuAppService: IDependency
	{
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="organizationMenuRequestDto"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(OrganizationMenuRequestDto organizationMenuRequestDto);
		/// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="organizationMenuRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchCreateAsync(IList<OrganizationMenuRequestDto> organizationMenuRequestDtos);

        /// <summary>
        /// 批量
        /// </summary>
        /// <param name="organizationMenuRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchRemoveAsync(IList<OrganizationMenuRequestDto> organizationMenuRequestDtos);

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="organizationMenuPageRequestDto"></param>
        /// <returns></returns>
        Task<PageData> GetPageListAsync(OrganizationMenuPageRequestDto organizationMenuPageRequestDto);
		/// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        Task<OrganizationMenuQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest);
		/// <summary>
        /// 修改
        /// </summary>
        /// <param name="organizationMenuRequestDto"></param>
        /// <returns></returns>
        Task<bool> ModifyAsync(OrganizationMenuRequestDto organizationMenuRequestDto);
		/// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="organizationMenuRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchModifyAsync(IList<OrganizationMenuRequestDto> organizationMenuRequestDtos);
		/// <summary>
        /// 删除
        /// </summary>
        /// <param name="entityRequest"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(EntityRequest entityRequest);

        /// <summary>
        /// 根据机构orgIds获取菜单
        /// </summary>
        /// <param name="orgIds"></param>
        /// <returns></returns>
        Task<IList<OrganizationMenuQueryDto>> GetMenuByOrgIdsAsync(IList<string> orgIds);
        /// <summary>
        /// 根据菜单Id获取主键
        /// </summary>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        Task<IList<string>> GetIdsByMenuIdsAsync(IList<string> menuIds);

        /// <summary>
        /// 根据机构organizationIds获取主键
        /// </summary>
        /// <param name="organizationIds"></param>
        /// <returns></returns>
        Task<IList<string>> GetIdsByOrganizationIdsAsync(IList<string> organizationIds);

    }
}