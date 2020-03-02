
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
	/// Organization -applaction实现
	/// </summary>
	public interface IOrganizationAppService: IDependency
	{
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="organizationRequestDto"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(OrganizationRequestDto organizationRequestDto);
		/// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="organizationRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchCreateAsync(IList<OrganizationRequestDto> organizationRequestDtos);
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="organizationPageRequestDto"></param>
        /// <returns></returns>
        Task<PageData> GetPageListAsync(OrganizationPageRequestDto organizationPageRequestDto);
		/// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        Task<OrganizationQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest);
		/// <summary>
        /// 修改
        /// </summary>
        /// <param name="organizationRequestDto"></param>
        /// <returns></returns>
        Task<bool> ModifyAsync(OrganizationRequestDto organizationRequestDto);
		/// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="organizationRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchModifyAsync(IList<OrganizationRequestDto> organizationRequestDtos);
		/// <summary>
        /// 删除
        /// </summary>
        /// <param name="entityRequest"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(EntityRequest entityRequest);

        /// <summary>
        /// 根据父级id获取集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IList<OrganizationQueryDto>> GetOrganizationByParentId(OrganizationParentRequestDto input);

        /// <summary>
        /// 根据父级id获取集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IList<OrganizationQueryDto>> GetChildrenByParentIds(List<string > parentIds);

        /// <summary>
        /// 根据id获取名称
        /// </summary>
        /// <param name="orgIds"></param>
        /// <returns></returns>
        Task<IList<OrganizationQueryDto>> GetOrganizationNameByIds(IList<string> orgIds);


        /// <summary>
        /// 查询是否有下级机构
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        Task<bool> GetAnyByParentIdAsync(string organizationId);


    }
}