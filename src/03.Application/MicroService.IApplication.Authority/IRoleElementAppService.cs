
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
	/// RoleElement -applaction实现
	/// </summary>
	public interface IRoleElementAppService: IDependency
	{
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="roleElementRequestDto"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(RoleElementRequestDto roleElementRequestDto);
		/// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="roleElementRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchCreateAsync(IList<RoleElementRequestDto> roleElementRequestDtos);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="roleElementRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchRemoveAsync(IList<RoleElementRequestDto> roleElementRequestDtos);
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="roleElementPageRequestDto"></param>
        /// <returns></returns>
        Task<PageData> GetPageListAsync(RoleElementPageRequestDto roleElementPageRequestDto);
		/// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        Task<RoleElementQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest);
		/// <summary>
        /// 修改
        /// </summary>
        /// <param name="roleElementRequestDto"></param>
        /// <returns></returns>
        Task<bool> ModifyAsync(RoleElementRequestDto roleElementRequestDto);
		/// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="roleElementRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchModifyAsync(IList<RoleElementRequestDto> roleElementRequestDtos);
		/// <summary>
        /// 删除
        /// </summary>
        /// <param name="entityRequest"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(EntityRequest entityRequest);
        /// <summary>
        /// 根据roleId获取模块元素
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        Task<IList<RoleElementQueryDto>> GetElementByRoleIdsAsync(IList<string> roleIds);
        /// <summary>
        /// 根据模块元素Id获取主键
        /// </summary>
        /// <param name="moduleElementIds"></param>
        /// <returns></returns>
        Task<IList<string>> GetIdsByModuleElementIdsAsync(IList<string> moduleElementIds);

        /// 根据roleIds获取主键
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        Task<IList<string>> GetIdsByRoleIdsAsync(IList<string> roleIds);
    }
}