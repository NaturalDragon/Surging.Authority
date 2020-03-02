using MicroService.Core;
using MicroService.Data.Common;
using MicroService.Data.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.IApplication.Authority.IDomainService
{
   public interface IRoleDomainService: IDependency
    {
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResponse> RemoveRole(EntityRequest input);
    }
}
