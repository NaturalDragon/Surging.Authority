
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
	/// RelationOrganizationEmployee -applaction实现
	/// </summary>
	public interface IRelationOrganizationEmployeeAppService: IDependency
	{
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="relationOrganizationEmployeeRequestDto"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(RelationOrganizationEmployeeRequestDto relationOrganizationEmployeeRequestDto);
		/// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="relationOrganizationEmployeeRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchCreateAsync(IList<RelationOrganizationEmployeeRequestDto> relationOrganizationEmployeeRequestDtos);
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="relationOrganizationEmployeePageRequestDto"></param>
        /// <returns></returns>
        Task<PageData> GetPageListAsync(RelationOrganizationEmployeePageRequestDto relationOrganizationEmployeePageRequestDto);
		/// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        Task<RelationOrganizationEmployeeQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest);
		/// <summary>
        /// 修改
        /// </summary>
        /// <param name="relationOrganizationEmployeeRequestDto"></param>
        /// <returns></returns>
        Task<bool> ModifyAsync(RelationOrganizationEmployeeRequestDto relationOrganizationEmployeeRequestDto);
		/// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="relationOrganizationEmployeeRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchModifyAsync(IList<RelationOrganizationEmployeeRequestDto> relationOrganizationEmployeeRequestDtos);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entityRequest"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(EntityRequest entityRequest);

        /// <summary>
        /// 根据用户Id和orgId删除
        /// </summary>
        /// <param name="relationOrganizationEmployeeRemoveDto"></param>
        /// <returns></returns>
        Task<bool> RemoveByEmployeeIdAndOrgIds(RelationOrganizationEmployeeRemoveDto relationOrganizationEmployeeRemoveDto);
        /// <summary>
        /// 根据机构ids查找empIds
        /// </summary>
        /// <param name="orgIds"></param>
        /// <returns></returns>
        Task<IList<string>> GetEmployeeIdsByOrgIds(IList<string> orgIds);

        /// <summary>
        /// 根据成员ids获取对应的机构id
        /// </summary>
        /// <param name="employeeIds"></param>
        /// <returns></returns>
        Task<IList<RelationOrganizationEmployeeQueryDto>> GetOrgIdsByEmployeeIds(IList<string> employeeIds);

        /// <summary>
        /// 查询该机构是否存在
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        Task<bool> GetAnyByOrganizationIdAsync(string organizationId);

        /// <summary>
        /// 根据employeeIds获取主键
        /// </summary>
        /// <param name="employeeIds"></param>
        /// <returns></returns>
        Task<IList<string>> GetIdsByEmployeeIds(IList<string> employeeIds);
    }
}