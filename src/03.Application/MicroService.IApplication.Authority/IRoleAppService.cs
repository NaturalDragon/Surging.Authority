
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
	/// Role -applaction实现
	/// </summary>
	public interface IRoleAppService: IDependency
	{
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="roleRequestDto"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(RoleRequestDto roleRequestDto);
		/// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="roleRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchCreateAsync(IList<RoleRequestDto> roleRequestDtos);
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="rolePageRequestDto"></param>
        /// <returns></returns>
        Task<PageData> GetPageListAsync(RolePageRequestDto rolePageRequestDto);
		/// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        Task<RoleQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest);
		/// <summary>
        /// 修改
        /// </summary>
        /// <param name="roleRequestDto"></param>
        /// <returns></returns>
        Task<bool> ModifyAsync(RoleRequestDto roleRequestDto);
		/// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="roleRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchModifyAsync(IList<RoleRequestDto> roleRequestDtos);
		/// <summary>
        /// 删除
        /// </summary>
        /// <param name="entityRequest"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(EntityRequest entityRequest);

        Task<IList<RoleTypeViewResponse>> GetRoleTypeList();

        /// <summary>
        /// 根据id获取名称
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        Task<IList<RoleQueryDto>> GetRoleNameByIds(IList<string> roleIds);

    }
}