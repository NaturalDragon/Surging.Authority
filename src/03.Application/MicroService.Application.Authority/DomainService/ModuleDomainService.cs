using MicroService.Common.Core.Enums;
using MicroService.Data.Common;
using MicroService.Data.Extensions;
using MicroService.Data.Validation;
using MicroService.IApplication.Authority;
using MicroService.IApplication.Authority.Dto;
using MicroService.IApplication.Authority.IDomainService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Application.Authority.DomainService
{
    public class ModuleDomainService : IModuleDomainService
    {
        private readonly IModuleAppService _moduleAppService;
        private readonly IModuleElementAppService _moduleElementAppService;
        private readonly IOrganizationElementAppService _organizationElementAppService;
        private readonly IRoleElementAppService _roleElementAppService;
        private readonly IEmployeeElementAppService _employeeElementAppService;
        private readonly ApplicationEnginee _applicationEnginee;

        public ModuleDomainService(IModuleAppService moduleAppService, IModuleElementAppService moduleElementAppService,
            IOrganizationElementAppService organizationElementAppService, IRoleElementAppService roleElementAppService,
            IEmployeeElementAppService employeeElementAppService)
        {
            _moduleAppService = moduleAppService;
            _moduleElementAppService = moduleElementAppService;
            _organizationElementAppService = organizationElementAppService;
            _roleElementAppService = roleElementAppService;
            _employeeElementAppService = employeeElementAppService;
            _applicationEnginee = new ApplicationEnginee ();
        }

        public async Task<JsonResponse> SaveModule(ModuleRequestDto input)
        {
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await AddOrModifyModule(input);
                var elmentRequests = input.ModuleElementActionRequests.ToList();
                await AddModuleElement(elmentRequests);
                await ModifyModuleElement(elmentRequests);
                await RemoveModuleElement(elmentRequests);

            });
            return resJson;
        }

        public async Task<ModuleQueryDto> GetModuleDetail(EntityQueryRequest input)
        {
            var module = await _moduleAppService.GetForModifyAsync(input);
            module.OperationStatus = OperationModel.Modify;
            var elements = await _moduleElementAppService.GetElementByModuleIds(new List<string>() { input.Id });
            module.ModuleElementActionRequests = elements;
            return module;
        }

       public async Task<IList<ModuleQueryDto>> GetModulesWithElements(EntityRequest input)
        {
            var modules =await _moduleAppService.GetAll();
            var moduleIds = modules.Select(m => m.Id).ToList();
            var moduleElements = await _moduleElementAppService.GetElementByModuleIds(moduleIds);
            foreach (var item in modules)
            {
                item.ModuleElementActionRequests = moduleElements.Where(e => e.ModuleId == item.Id).ToList();
            }
            return modules;
        }
        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="entityRequest"></param>
        /// <returns></returns>
        public async Task<JsonResponse> RemoveModule(EntityRequest entityRequest)
        {
            var elementDtos= await _moduleElementAppService.GetElementByModuleIds(entityRequest.Ids); ;
            var elementIds = elementDtos.Select(r => r.Id).ToList();
            var orgElementIds = await _organizationElementAppService.GetIdsByModuleElementIdsAsync(elementIds);
            var roleElementIds = await _roleElementAppService.GetIdsByModuleElementIdsAsync(elementIds);
            var empElementIds = await _employeeElementAppService.GetIdsByModuleElementIdsAsync(elementIds);
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _moduleAppService.RemoveAsync(entityRequest);
                await _moduleElementAppService.RemoveAsync(new EntityRequest()
                {
                    Ids = elementIds,
                    CreateDate = DateTime.Now,
                    CreateUserId = entityRequest.CreateUserId,
                    CreateUserName = entityRequest.CreateUserName
                });
                await _organizationElementAppService.RemoveAsync(new EntityRequest()
                {
                    Ids = orgElementIds,
                    CreateDate = DateTime.Now,
                    CreateUserId = entityRequest.CreateUserId,
                    CreateUserName = entityRequest.CreateUserName
                });
                await _roleElementAppService.RemoveAsync(new EntityRequest()
                {
                    Ids = roleElementIds,
                    CreateDate = DateTime.Now,
                    CreateUserId = entityRequest.CreateUserId,
                    CreateUserName = entityRequest.CreateUserName
                });
                await _employeeElementAppService.RemoveAsync(new EntityRequest()
                {
                    Ids = empElementIds,
                    CreateDate = DateTime.Now,
                    CreateUserId = entityRequest.CreateUserId,
                    CreateUserName = entityRequest.CreateUserName
                });
            });
            return resJson;
        }
        #region private
        private async Task AddOrModifyModule(ModuleRequestDto moduleRequestDto)
        {
            if (moduleRequestDto.OperationStatus == OperationModel.Create)
            {
                await _moduleAppService.CreateAsync(moduleRequestDto);
            }
            else if (moduleRequestDto.OperationStatus == OperationModel.Modify)
            {
                await _moduleAppService.ModifyAsync(moduleRequestDto);
               
            }
        }
        private async Task AddModuleElement(List<ModuleElementRequestDto> moduleElementRequestDtos)
        {
            var elements = moduleElementRequestDtos.Where(m => m.OperationStatus == OperationModel.Create).ToList();
            if (elements.Any())
            {
                await _moduleElementAppService.BatchCreateAsync(elements);
            }
        }

        private async Task ModifyModuleElement(List<ModuleElementRequestDto> moduleElementRequestDtos)
        {
            var elements = moduleElementRequestDtos.Where(m => m.OperationStatus == OperationModel.Modify).ToList();
            if (elements.Any())
            {
                await _moduleElementAppService.BatchModifyAsync(elements);
            }
        }
        private async Task RemoveModuleElement(List<ModuleElementRequestDto> moduleElementRequestDtos)
        {
            var elements = moduleElementRequestDtos.Where(m => m.OperationStatus == OperationModel.Delete).ToList();
            if (elements.Any())
            {
                var ids = elements.Select(e => e.Id).ToList();
                var entityRequest = new EntityRequest() { Ids = ids, Payload = elements.First().Payload };
                entityRequest.InitModifyRequest();
                await _moduleElementAppService.RemoveAsync(entityRequest);
            }
        }
        #endregion
    }
}
