using MicroService.Core;
using MicroService.Data.Validation;
using MicroService.IApplication.Authority.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.IApplication.Authority.IDomainService
{
    public interface IModuleElementDomainService : IDependency
    {
        /// <summary>
        /// 保存机构模块元素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResponse> SaveOrganizationModuleElement(OrganizationElementRequestDto input);
        /// <summary>
        /// 保存角色模块元素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResponse> SaveRoleModuleElement(RoleElementRequestDto input);
        /// <summary>
        /// 保存成员模块元素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResponse> SaveEmployeeModuleElement(EmployeeElementRequestDto input);

        /// <summary>
        /// 获取机构模块元素信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IList<PowerSelectViewResponse>> GetOrganizationModuleElement(OrganizationElementRequestDto input);
        /// <summary>
        /// 获取机构模块元素信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IList<PowerSelectViewResponse>> GetRoleModuleElement(RoleElementRequestDto input);
        
    }
}
