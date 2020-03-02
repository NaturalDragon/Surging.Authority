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
    class RoleDomainService : IRoleDomainService
    {
        private readonly ApplicationEnginee _applicationEnginee;
        private readonly IRelationEmployeeRoleAppService _relationEmployeeRoleApp;
        private readonly IRoleAppService _roleAppService;
        private readonly IRoleMenuAppService  _roleMenuAppService;
        private readonly IRoleElementAppService _roleElementAppService;

        public RoleDomainService(IRelationEmployeeRoleAppService relationEmployeeRoleApp,IRoleAppService roleAppService, IRoleMenuAppService roleMenuAppService,
            IRoleElementAppService roleElementAppService)
        {
            _applicationEnginee = new ApplicationEnginee();
            _relationEmployeeRoleApp = relationEmployeeRoleApp;
            _roleAppService = roleAppService;
            _roleMenuAppService = roleMenuAppService;
            _roleElementAppService = roleElementAppService;
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> RemoveRole(EntityRequest input)
        {
            var exitsMap = await _relationEmployeeRoleApp.GetAnyByRoleIdAsync(input.Id);
            if (exitsMap)
            {
                return JsonResponse.Create(false, "该角色下有成员");
            }
            input.Ids = new List<string>() { input.Id };
            var roleMenuIds = await _roleMenuAppService.GetIdsByRoleIdsAsync(new List<string>() { input.Id });
            var roleElementIds = await  _roleElementAppService.GetIdsByRoleIdsAsync(new List<string>() { input.Id });
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _roleAppService.RemoveAsync(input);
                await _roleMenuAppService.RemoveAsync(new EntityRequest()
                {
                    Ids = roleMenuIds,
                    ModifyDate = DateTime.Now,
                    ModifyUserId = input.ModifyUserId,
                    ModifyUserName = input.ModifyUserName
                });
                await _roleElementAppService.RemoveAsync(new EntityRequest()
                {
                    Ids = roleElementIds,
                    ModifyDate = DateTime.Now,
                    ModifyUserId = input.ModifyUserId,
                    ModifyUserName = input.ModifyUserName
                });

            });
            return resJson;
        }
    }
}
