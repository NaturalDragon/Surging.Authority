using MicroService.Common.Models;
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
   public interface IEmployeeDomainService: IDependency
    {

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<LoginUser> Login(EmployeeRequestDto input);

        /// <summary>
        /// 获取成员当前菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IList<AntMenuTree>> GetCurrentMenuTree(EmployeeMenuRequestDto input);

        /// <summary>
        /// 获取成员信息分页
        /// </summary>
        /// <param name="employeePageRequestDto"></param>
        /// <returns></returns>

        Task<PageData> GetListPagedByOrgIdOrRoleId(EmployeePageRequestDto employeePageRequestDto);

        Task<PageData> GetListPagedOriginal(EmployeePageOriginalRequestDto employeePageOriginalRequestDto);

        /// <summary>
        /// 获取机构模块元素信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IList<PowerSelectViewResponse>> GetEmployeeModuleElement(EmployeeElementRequestDto input);

        /// <summary>
        /// 获取某成员菜单权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IList<PowerSelectViewResponse>> GetEmployeeMenu(EmployeeMenuRequestDto input);

        /// <summary>
        /// 保存成员
        /// </summary>
        /// <param name="employeeRequestDto"></param>
        /// <returns></returns>
        Task<JsonResponse> SaveEmployee(EmployeeRequestDto employeeRequestDto);

        /// <summary>
        /// 获取成员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<EmployeeQueryDto> GetEmployee(EntityQueryRequest input);

        /// <summary>
        /// 删除成员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResponse> RemoveEmployee(EntityRequest entityRequest);

    }
}
