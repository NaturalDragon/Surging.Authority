
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
	/// RelationEmployeeRole -applaction实现
	/// </summary>
	public interface IRelationEmployeeRoleAppService: IDependency
	{
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="relationEmployeeRoleRequestDto"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(RelationEmployeeRoleRequestDto relationEmployeeRoleRequestDto);
		/// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="relationEmployeeRoleRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchCreateAsync(IList<RelationEmployeeRoleRequestDto> relationEmployeeRoleRequestDtos);
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="relationEmployeeRolePageRequestDto"></param>
        /// <returns></returns>
        Task<PageData> GetPageListAsync(RelationEmployeeRolePageRequestDto relationEmployeeRolePageRequestDto);
		/// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        Task<RelationEmployeeRoleQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest);
		/// <summary>
        /// 修改
        /// </summary>
        /// <param name="relationEmployeeRoleRequestDto"></param>
        /// <returns></returns>
        Task<bool> ModifyAsync(RelationEmployeeRoleRequestDto relationEmployeeRoleRequestDto);
		/// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="relationEmployeeRoleRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchModifyAsync(IList<RelationEmployeeRoleRequestDto> relationEmployeeRoleRequestDtos);
		/// <summary>
        /// 删除
        /// </summary>
        /// <param name="entityRequest"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(EntityRequest entityRequest);

        /// <summary>
        /// 根据成员Id和角色ids删除
        /// </summary>
        /// <param name="relationEmployeeRoleRemoveDto"></param>
        /// <returns></returns>
        Task<bool> RemoveByEmployeeIdAndRoleIds(RelationEmployeeRoleRemoveDto relationEmployeeRoleRemoveDto);

        /// <summary>
        /// 根据角色ids查找empIds
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        Task<IList<string>> GetEmployeeIdsByRoleIds(IList<string> roleIds);

        /// <summary>
        /// 根据成员id获取角色Ids
        /// </summary>
        /// <param name="employeeIds"></param>
        /// <returns></returns>
        Task<IList<RelationEmployeeRoleQueryDto>> GetRoleIdsByEmployeeIds(IList<string> employeeIds);

        /// <summary>
        /// 查询该角色是否存在
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<bool> GetAnyByRoleIdAsync(string roleId);

        /// <summary>
        /// 根据employeeIds获取主键
        /// </summary>
        /// <param name="employeeIds"></param>
        /// <returns></returns>
        Task<IList<string>> GetIdsByEmployeeIds(IList<string> employeeIds);
        
    }
}