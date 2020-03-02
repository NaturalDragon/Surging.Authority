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
    public class ModuleElementDomainService : IModuleElementDomainService
    {
        private readonly ApplicationEnginee _applicationEnginee;
        private readonly IOrganizationElementAppService _organizationElementAppService;
        private readonly IRoleElementAppService _roleElementAppService;
        private readonly IEmployeeElementAppService _employeeElementAppService;
        private readonly IRelationOrganizationEmployeeAppService _relationOrganizationEmployeeAppService;
        private readonly IRelationEmployeeRoleAppService _relationEmployeeRoleAppService;
        public ModuleElementDomainService(IOrganizationElementAppService organizationElementAppService, IRoleElementAppService roleElementAppService,
            IEmployeeElementAppService employeeElementAppService, IRelationOrganizationEmployeeAppService relationOrganizationEmployeeAppService,
            IRelationEmployeeRoleAppService relationEmployeeRoleAppService)
        {
            _applicationEnginee = new ApplicationEnginee();
            _organizationElementAppService = organizationElementAppService;
            _roleElementAppService = roleElementAppService;
            _employeeElementAppService = employeeElementAppService;
            _relationOrganizationEmployeeAppService = relationOrganizationEmployeeAppService;
            _relationEmployeeRoleAppService = relationEmployeeRoleAppService;
        }

        /// <summary>
        /// 保存机构模块元素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> SaveOrganizationModuleElement(OrganizationElementRequestDto input)
        {
            var result = await GetAddOrRemoveOrganizationElementRequest(input);
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _organizationElementAppService.BatchCreateAsync(result.Item1);
                await _organizationElementAppService.BatchRemoveAsync(result.Item2);
            });
            return resJson;
        }
        /// <summary>
        /// 保存角色模块元素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> SaveRoleModuleElement(RoleElementRequestDto input)
        {
            var result = await GetAddOrRemoveRoleElementRequest(input);
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _roleElementAppService.BatchCreateAsync(result.Item1);
                await _roleElementAppService.BatchRemoveAsync(result.Item2);
            });
            return resJson;
        }
        /// <summary>
        /// 保存成员模块元素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> SaveEmployeeModuleElement(EmployeeElementRequestDto input)
        {
            var result = await GetAddOrRemoveEmployeeElementRequest(input);
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _employeeElementAppService.BatchCreateAsync(result.Item1);
                await _employeeElementAppService.BatchRemoveAsync(result.Item2);
            });
            return resJson;
        }

        /// <summary>
        /// 获取机构模块元素信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IList<PowerSelectViewResponse>> GetOrganizationModuleElement(OrganizationElementRequestDto input)
        {
            var orgElementDtos = await _organizationElementAppService.GetElementByOrganizationIdsAsync(new List<string> { input.OrganizationId });
            return orgElementDtos.Select(o => new PowerSelectViewResponse { Id = o.ElementId }).ToList();
        }
        /// <summary>
        /// 获取机构模块元素信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        public async Task<IList<PowerSelectViewResponse>> GetRoleModuleElement(RoleElementRequestDto input)
        {
            var roleElementDtos = await _roleElementAppService.GetElementByRoleIdsAsync(new List<string> { input.RoleId });
            return roleElementDtos.Select(o => new PowerSelectViewResponse { Id = o.ElementId }).ToList();
        }
       
        #region private
        async Task<(IList<OrganizationElementRequestDto>, IList<OrganizationElementRequestDto>)> 
            GetAddOrRemoveOrganizationElementRequest(OrganizationElementRequestDto  organizationElementRequestDto)
        {
            //查询该机构所有模块元素
            var allExitsEelments = await _organizationElementAppService.GetElementByOrganizationIdsAsync(new List<string>
            { organizationElementRequestDto.OrganizationId });
            var allExitsElementIds = allExitsEelments.Select(m => m.ElementId).ToList();

            var addMenuIds = organizationElementRequestDto.ElementIds.Except(allExitsElementIds);
            var delMenuIds = allExitsElementIds.Except(organizationElementRequestDto.ElementIds);
            var addDto = addMenuIds.Select(m => new OrganizationElementRequestDto
            {
                Id = Guid.NewGuid().ToString(),
                ElementId = m,
                OrganizationId = organizationElementRequestDto.OrganizationId,
                CreateUserId = organizationElementRequestDto.CreateUserId,
                CreateUserName = organizationElementRequestDto.CreateUserName,
            });
            var delDto = delMenuIds.Select(m =>
             new OrganizationElementRequestDto
             {
                 ElementId = m,
                 OrganizationId = organizationElementRequestDto.OrganizationId,
                 IsDelete = true,
                 ModifyUserId = organizationElementRequestDto.ModifyUserId,
                 ModifyUserName = organizationElementRequestDto.ModifyUserName,
                 ModifyDate = DateTime.Now

             });
            return (addDto.ToList(), delDto.ToList());
        }

        async Task<(IList<RoleElementRequestDto>, IList<RoleElementRequestDto>)>
            GetAddOrRemoveRoleElementRequest(RoleElementRequestDto  roleElementRequestDto)
        {
            //查询该角色所有模块元素
            var allExitsEelments = await _roleElementAppService.GetElementByRoleIdsAsync(new List<string>
            { roleElementRequestDto.RoleId });
            var allExitsElementIds = allExitsEelments.Select(m => m.ElementId).ToList();

            var addMenuIds = roleElementRequestDto.ElementIds.Except(allExitsElementIds);
            var delMenuIds = allExitsElementIds.Except(roleElementRequestDto.ElementIds);
            var addDto = addMenuIds.Select(m => new RoleElementRequestDto
            {
                Id = Guid.NewGuid().ToString(),
                ElementId = m,
                RoleId = roleElementRequestDto.RoleId,
                CreateUserId = roleElementRequestDto.CreateUserId,
                CreateUserName = roleElementRequestDto.CreateUserName,
            });
            var delDto = delMenuIds.Select(m =>
             new RoleElementRequestDto
             {
                 ElementId = m,
                 RoleId = roleElementRequestDto.RoleId,
                 IsDelete = true,
                 ModifyUserId = roleElementRequestDto.ModifyUserId,
                 ModifyUserName = roleElementRequestDto.ModifyUserName,
                 ModifyDate = DateTime.Now

             });
            return (addDto.ToList(), delDto.ToList());
        }

        async Task<(IList<EmployeeElementRequestDto>, IList<EmployeeElementRequestDto>)>
          GetAddOrRemoveEmployeeElementRequest(EmployeeElementRequestDto employeeElementRequestDto)
        {
            //查询该角色所有模块元素
            var allExitsEelments = await _employeeElementAppService.GetElementByEmployeeIdAsync(employeeElementRequestDto.EmployeeId);
            var allExitsElementIds = allExitsEelments.Select(m => m.ElementId).ToList();

            var addMenuIds = employeeElementRequestDto.ElementIds.Except(allExitsElementIds);
            var delMenuIds = allExitsElementIds.Except(employeeElementRequestDto.ElementIds);
            var addDto = addMenuIds.Select(m => new EmployeeElementRequestDto
            {
                Id = Guid.NewGuid().ToString(),
                EmployeeId= employeeElementRequestDto.EmployeeId,
                ElementId = m,
                RoleId = employeeElementRequestDto.RoleId,
                CreateUserId = employeeElementRequestDto.CreateUserId,
                CreateUserName = employeeElementRequestDto.CreateUserName,
            });
            var delDto = delMenuIds.Select(m =>
             new EmployeeElementRequestDto
             {
                 ElementId = m,
                 EmployeeId = employeeElementRequestDto.EmployeeId,
                 RoleId = employeeElementRequestDto.RoleId,
                 IsDelete = true,
                 ModifyUserId = employeeElementRequestDto.ModifyUserId,
                 ModifyUserName = employeeElementRequestDto.ModifyUserName,
                 ModifyDate = DateTime.Now

             });
            return (addDto.ToList(), delDto.ToList());
        }
        #endregion
    }
}
