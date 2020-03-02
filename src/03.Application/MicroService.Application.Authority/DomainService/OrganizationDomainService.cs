using MicroService.Data.Common;
using MicroService.Data.Validation;
using MicroService.IApplication.Authority;
using MicroService.IApplication.Authority.IDomainService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Application.Authority.DomainService
{
   public class OrganizationDomainService: IOrganizationDomainService
    {
        private readonly IOrganizationAppService _organizationAppService;
        private readonly IRelationOrganizationEmployeeAppService _relationOrganizationEmployeeAppService;
        private readonly IOrganizationMenuAppService _organizationMenuAppService;
        private readonly IOrganizationElementAppService _organizationElementAppService;
        private readonly ApplicationEnginee _applicationEnginee;
        public OrganizationDomainService(IOrganizationAppService organizationAppService, IRelationOrganizationEmployeeAppService relationOrganizationEmployeeAppService,
            IOrganizationMenuAppService organizationMenuAppService, IOrganizationElementAppService organizationElementAppService)
        {
            _organizationAppService = organizationAppService;
            _relationOrganizationEmployeeAppService = relationOrganizationEmployeeAppService;
            _organizationMenuAppService = organizationMenuAppService;
            _organizationElementAppService = organizationElementAppService;
            _applicationEnginee = new ApplicationEnginee();
        }
        /// <summary>
        /// 删除机构
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> RemoveOrganization(EntityRequest input)
        {
            var existsChild = await _organizationAppService.GetAnyByParentIdAsync(input.Id);
            if (existsChild)
            {
                return JsonResponse.Create(false, "该机构有下级机构");
            }
            var exitsEmp = await _relationOrganizationEmployeeAppService.GetAnyByOrganizationIdAsync(input.Id);
            if (exitsEmp)
            {
                return JsonResponse.Create(false, "该机构有下有成员");
            }
            input.Ids = new List<string>() { input.Id };
           var orgMenuIds= await _organizationMenuAppService.GetIdsByOrganizationIdsAsync(new List<string>() { input.Id });
           var orgElementIds= await _organizationElementAppService.GetIdsByOrganizationIdsAsync(new List<string>() { input.Id });

            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _organizationAppService.RemoveAsync(input);
                await _organizationMenuAppService.RemoveAsync(new EntityRequest()
                {
                    Ids = orgMenuIds,
                    ModifyDate = DateTime.Now,
                    ModifyUserId = input.ModifyUserId,
                    ModifyUserName = input.ModifyUserName
                });
                await _organizationElementAppService.RemoveAsync(new EntityRequest()
                {
                    Ids = orgElementIds,
                    ModifyDate = DateTime.Now,
                    ModifyUserId = input.ModifyUserId,
                    ModifyUserName = input.ModifyUserName
                });

            });
            return resJson;
        }
    }
}
