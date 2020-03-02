using MicroService.Core;
using MicroService.Data.Common;
using MicroService.Data.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.IApplication.Authority.IDomainService
{
    public interface IOrganizationDomainService: IDependency
    {
        /// <summary>
        /// 删除机构
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResponse> RemoveOrganization(EntityRequest input);
    }
}
