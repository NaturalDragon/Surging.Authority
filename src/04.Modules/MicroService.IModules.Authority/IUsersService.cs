using MicroService.Common.Models;
using MicroService.IApplication.Authority.Dto;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.IModules.Authority
{
    // <summary>
    /// Users -IModule接口
    /// </summary>
    [ServiceBundle("api/{Service}")]
   public interface IUsersService: IServiceKey
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<LoginUser> Authentication(EmployeeRequestDto input);
    }
}
