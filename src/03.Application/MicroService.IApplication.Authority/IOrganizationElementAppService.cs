
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
	/// OrganizationElement -applaction实现
	/// </summary>
	public interface IOrganizationElementAppService: IDependency
	{
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="organizationElementRequestDto"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(OrganizationElementRequestDto organizationElementRequestDto);
		/// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="organizationElementRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchCreateAsync(IList<OrganizationElementRequestDto> organizationElementRequestDtos);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="organizationElementRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchRemoveAsync(IList<OrganizationElementRequestDto> organizationElementRequestDtos);
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="organizationElementPageRequestDto"></param>
        /// <returns></returns>
        Task<PageData> GetPageListAsync(OrganizationElementPageRequestDto organizationElementPageRequestDto);
		/// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        Task<OrganizationElementQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest);
		/// <summary>
        /// 修改
        /// </summary>
        /// <param name="organizationElementRequestDto"></param>
        /// <returns></returns>
        Task<bool> ModifyAsync(OrganizationElementRequestDto organizationElementRequestDto);
		/// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="organizationElementRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchModifyAsync(IList<OrganizationElementRequestDto> organizationElementRequestDtos);
		/// <summary>
        /// 删除
        /// </summary>
        /// <param name="entityRequest"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(EntityRequest entityRequest);

        /// <summary>
        /// 根据机构ids获取模块元素
        /// </summary>
        /// <param name="organizationIds"></param>
        /// <returns></returns>
        Task<IList<OrganizationElementQueryDto>> GetElementByOrganizationIdsAsync(IList<string> organizationIds);

        /// <summary>
        /// 根据模块元素Id获取主键
        /// </summary>
        /// <param name="moduleElementIds"></param>
        /// <returns></returns>
        Task<IList<string>> GetIdsByModuleElementIdsAsync(IList<string> moduleElementIds);

        /// <summary>
        /// 根据organizationIds获取主键
        /// </summary>
        /// <param name="organizationIds"></param>
        /// <returns></returns>
        Task<IList<string>> GetIdsByOrganizationIdsAsync(IList<string> organizationIds);
    }
}