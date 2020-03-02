
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
	/// RoleMenu -applaction实现
	/// </summary>
	public interface IRoleMenuAppService: IDependency
	{
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="roleMenuRequestDto"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(RoleMenuRequestDto roleMenuRequestDto);
		/// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="roleMenuRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchCreateAsync(IList<RoleMenuRequestDto> roleMenuRequestDtos);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="roleMenuRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchRemoveAsync(IList<RoleMenuRequestDto> roleMenuRequestDtos);
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="roleMenuPageRequestDto"></param>
        /// <returns></returns>
        Task<PageData> GetPageListAsync(RoleMenuPageRequestDto roleMenuPageRequestDto);
		/// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        Task<RoleMenuQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest);
		/// <summary>
        /// 修改
        /// </summary>
        /// <param name="roleMenuRequestDto"></param>
        /// <returns></returns>
        Task<bool> ModifyAsync(RoleMenuRequestDto roleMenuRequestDto);
		/// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="roleMenuRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchModifyAsync(IList<RoleMenuRequestDto> roleMenuRequestDtos);
		/// <summary>
        /// 删除
        /// </summary>
        /// <param name="entityRequest"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(EntityRequest entityRequest);

        /// <summary>
        /// 根据roleIds获取菜单
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        Task<IList<RoleMenuQueryDto>> GetMenuByRoleIdsAsync(IList<string> roleIds);


        /// <summary>
        /// 根据菜单Id获取主键
        /// </summary>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        Task<IList<string>> GetIdsByMenuIdsAsync(IList<string> menuIds);

        /// 根据roleIds获取主键
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        Task<IList<string>> GetIdsByRoleIdsAsync(IList<string> roleIds);
    }
}