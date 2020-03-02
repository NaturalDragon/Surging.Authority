using MicroService.Common.Core.Enums;
using MicroService.Common.Models;
using MicroService.Data.Validation;
using MicroService.IApplication.Authority;
using MicroService.IApplication.Authority.Dto;
using MicroService.IApplication.Authority.IDomainService;
using MicroService.IModules.Authority;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.ProxyGenerator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Modules.Authority
{
    /// <summary>
    /// Users -Module实现
    /// </summary>
    [ModuleName("Users")]
     public class UsersService : ProxyServiceBase, IUsersService
    {
        private readonly IEmployeeAppService _employeeAppService;
        private readonly IEmployeeDomainService _employeeDomainService;
        private readonly ApplicationEnginee _applicationEnginee;
        public UsersService(IEmployeeAppService employeeAppService, IEmployeeDomainService employeeDomainService)
        {
            _employeeAppService = employeeAppService;
            _employeeDomainService = employeeDomainService;
            _applicationEnginee = new ApplicationEnginee();
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<LoginUser> Authentication(EmployeeRequestDto input)
        {
           return await _employeeDomainService.Login(input);
          //  return await Task.FromResult(new LoginUser()
          //  {
          //      IsSucceed = true,
          //      UserId = Guid.NewGuid().ToString(),
          //      Name = "admin",
          //      RoleType = RoleType.AdministratorRole
          //  });
          ////  return await _usersAppService.Login(input);
        }
    }
}
