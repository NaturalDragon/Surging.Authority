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
   public interface IModuleDomainService: IDependency
    {
       Task<JsonResponse> SaveModule(ModuleRequestDto input);


        Task<ModuleQueryDto> GetModuleDetail(EntityQueryRequest input);

        Task<IList<ModuleQueryDto>> GetModulesWithElements(EntityRequest input);
        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="entityRequest"></param>
        /// <returns></returns>
        Task<JsonResponse> RemoveModule(EntityRequest  entityRequest);
    }
}
