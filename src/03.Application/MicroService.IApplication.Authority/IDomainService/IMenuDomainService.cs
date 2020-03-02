using MicroService.Core;
using MicroService.Data.Common;
using MicroService.Data.Validation;
using MicroService.IApplication.Authority.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.IApplication.Authority.IDomainService
{
   public interface IMenuDomainService: IDependency
    {
        /// <summary>
        /// 保存机构菜单权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResponse> SaveOrgMenu(OrganizationMenuRequestDto input);

        /// <summary>
        /// 保存角色菜单权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResponse> SaveRoleMenu(RoleMenuRequestDto input);

        /// <summary>
        /// 保存人员菜单权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResponse> SaveEmployeeMenu(EmployeeMenuRequestDto input);

        /// <summary>
        /// 获取某机构菜单权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IList<PowerSelectViewResponse>> GetOrgMenu(OrganizationMenuRequestDto input);

        /// <summary>
        /// 获取某角色菜单权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IList<PowerSelectViewResponse>> GetRoleMenu(RoleMenuRequestDto input);

      

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResponse> RemoveMenu(EntityRequest input);

       
    }
}
