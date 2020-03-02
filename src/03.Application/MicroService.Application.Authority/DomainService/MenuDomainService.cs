using MicroService.Data.Common;
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
    public class MenuDomainService : IMenuDomainService
    {
        private readonly ApplicationEnginee _applicationEnginee;
        private readonly IOrganizationMenuAppService _organizationMenuAppService;
        private readonly IRoleMenuAppService _roleMenuAppService;
        private readonly IEmployeeMenuAppService _employeeMenuAppService;
        private readonly IRelationOrganizationEmployeeAppService _relationOrganizationEmployeeAppService;
        private readonly IRelationEmployeeRoleAppService _relationEmployeeRoleAppService;
        private readonly IMenuAppService _menuAppService;
        public MenuDomainService(IOrganizationMenuAppService organizationMenuAppService, IRoleMenuAppService roleMenuAppService,
            IEmployeeMenuAppService employeeMenuAppService, IRelationOrganizationEmployeeAppService relationOrganizationEmployeeAppService,
            IRelationEmployeeRoleAppService relationEmployeeRoleAppService, IMenuAppService menuAppService)
        {
            _applicationEnginee = new ApplicationEnginee();
            _organizationMenuAppService = organizationMenuAppService;
            _roleMenuAppService = roleMenuAppService;
            _employeeMenuAppService = employeeMenuAppService;
            _relationOrganizationEmployeeAppService = relationOrganizationEmployeeAppService;
            _relationEmployeeRoleAppService = relationEmployeeRoleAppService;
            _menuAppService = menuAppService;

        }

        /// <summary>
        /// 保存机构菜单权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> SaveOrgMenu(OrganizationMenuRequestDto input)
        {
            var result = await GetAddOrRemoveOrganizationMenuRequest(input);
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _organizationMenuAppService.BatchCreateAsync(result.Item1);
                await _organizationMenuAppService.BatchRemoveAsync(result.Item2);
            });
            return resJson;
        }

        /// <summary>
        /// 保存角色菜单权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> SaveRoleMenu(RoleMenuRequestDto input)
        {
            var result = await GetAddOrRemoveRoleMenuAction(input);
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _roleMenuAppService.BatchCreateAsync(result.Item1);
                await _roleMenuAppService.BatchRemoveAsync(result.Item2);
            });
            return resJson;
        }

        /// <summary>
        /// 保存人员菜单权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> SaveEmployeeMenu(EmployeeMenuRequestDto input)
        {
            var result = await GetAddOrRemoveEmployeeMenuAction(input);
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _employeeMenuAppService.BatchCreateAsync(result.Item1);
                await _employeeMenuAppService.BatchRemoveAsync(result.Item2);
            });
            return resJson;
        }

        /// <summary>
        /// 获取某机构菜单权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IList<PowerSelectViewResponse>> GetOrgMenu(OrganizationMenuRequestDto input)
        {
            var orgMenuDtos = await _organizationMenuAppService.GetMenuByOrgIdsAsync(new List<string> { input.OrganizationId });
            return orgMenuDtos.Select(o => new PowerSelectViewResponse { Id = o.MenuId }).ToList();
        }

        /// <summary>
        /// 获取某角色菜单权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IList<PowerSelectViewResponse>> GetRoleMenu(RoleMenuRequestDto input)
        {
            var roleMenuDtos = await _roleMenuAppService.GetMenuByRoleIdsAsync(new List<string> { input.RoleId });
            return roleMenuDtos.Select(o => new PowerSelectViewResponse { Id = o.MenuId }).ToList();
        }

      
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> RemoveMenu(EntityRequest input)
        {
            var orgMenuIds = await _organizationMenuAppService.GetIdsByMenuIdsAsync(input.Ids);
            var roleMenuIds = await _roleMenuAppService.GetIdsByMenuIdsAsync(input.Ids);
            var empMenuIds = await _employeeMenuAppService.GetIdsByMenuIdsAsync(input.Ids);
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _menuAppService.RemoveAsync(input);
                await _organizationMenuAppService.RemoveAsync(new EntityRequest()
                {
                    Ids = orgMenuIds,
                    ModifyDate = DateTime.Now,
                    ModifyUserId = input.ModifyUserId,
                    ModifyUserName = input.ModifyUserName
                });
                await _roleMenuAppService.RemoveAsync(new EntityRequest()
                {
                    Ids = roleMenuIds,
                    ModifyDate = DateTime.Now,
                    ModifyUserId = input.ModifyUserId,
                    ModifyUserName = input.ModifyUserName
                });
                await _employeeMenuAppService.RemoveAsync(new EntityRequest()
                {
                    Ids = empMenuIds,
                    ModifyDate = DateTime.Now,
                    ModifyUserId = input.ModifyUserId,
                    ModifyUserName = input.ModifyUserName
                });

            });
            return resJson;
        }


        #region pravite
        async Task<(IList<OrganizationMenuRequestDto>, IList<OrganizationMenuRequestDto>)> GetAddOrRemoveOrganizationMenuRequest(OrganizationMenuRequestDto organizationMenuRequestDto)
        {
            //查询该机构所有菜单
            var allExitsMenu = await _organizationMenuAppService.GetMenuByOrgIdsAsync(new List<string> { organizationMenuRequestDto.OrganizationId });
            var allExitsMenuIds = allExitsMenu.Select(m => m.MenuId).ToList();

            var addMenuIds = organizationMenuRequestDto.MenuIds.Except(allExitsMenuIds);
            var delMenuIds = allExitsMenuIds.Except(organizationMenuRequestDto.MenuIds);
            var addDto = addMenuIds.Select(m => new OrganizationMenuRequestDto
            {
                Id = Guid.NewGuid().ToString(),
                MenuId = m,
                OrganizationId = organizationMenuRequestDto.OrganizationId,
                CreateUserId = organizationMenuRequestDto.CreateUserId,
                CreateUserName = organizationMenuRequestDto.CreateUserName,
            });
            var delDto = delMenuIds.Select(m =>
             new OrganizationMenuRequestDto
             {
                 MenuId = m,
                 OrganizationId = organizationMenuRequestDto.OrganizationId,
                 IsDelete = true,
                 ModifyUserId = organizationMenuRequestDto.ModifyUserId,
                 ModifyUserName = organizationMenuRequestDto.ModifyUserName,
                 ModifyDate = DateTime.Now

             });
            return (addDto.ToList(),delDto.ToList());
        }
        async Task<(IList<RoleMenuRequestDto>, IList<RoleMenuRequestDto>)> GetAddOrRemoveRoleMenuAction(RoleMenuRequestDto roleMenuRequestDto)
        {
            //查询该角色所有菜单
            var allExitsMenu = await _roleMenuAppService.GetMenuByRoleIdsAsync(new List<string> { roleMenuRequestDto.RoleId });
            var allExitsMenuIds = allExitsMenu.Select(m => m.MenuId).ToList();

            var addMenuIds = roleMenuRequestDto.MenuIds.Except(allExitsMenuIds);
            var delMenuIds = allExitsMenuIds.Except(roleMenuRequestDto.MenuIds);
            var addDto = addMenuIds.Select(m => new RoleMenuRequestDto
            {
                Id = Guid.NewGuid().ToString(),
                MenuId = m,
                RoleId = roleMenuRequestDto.RoleId,
                CreateUserId = roleMenuRequestDto.CreateUserId,
                CreateUserName = roleMenuRequestDto.CreateUserName,
            });
            var delDto = delMenuIds.Select(m =>
             new RoleMenuRequestDto
             {
                 MenuId = m,
                 RoleId = roleMenuRequestDto.RoleId,
                 IsDelete=false,
                 ModifyUserId = roleMenuRequestDto.ModifyUserId,
                 ModifyUserName = roleMenuRequestDto.ModifyUserName,
                 ModifyDate = DateTime.Now
             });
            return (addDto.ToList(),delDto.ToList());
        }

        async Task<(IList<EmployeeMenuRequestDto>, IList<EmployeeMenuRequestDto>)> GetAddOrRemoveEmployeeMenuAction(EmployeeMenuRequestDto employeeMenuRequestDto)
        {
            //查询该机构所有菜单
            var allExitsMenu = await _employeeMenuAppService.GetMenuByEmployeeIdAsync(employeeMenuRequestDto.EmployeeId);
            var allExitsMenuIds = allExitsMenu.Select(m => m.MenuId).ToList();

            var addMenuIds = employeeMenuRequestDto.MenuIds.Except(allExitsMenuIds);
            var delMenuIds = allExitsMenuIds.Except(employeeMenuRequestDto.MenuIds);

            var removeEmpMenu = allExitsMenu.Where(e => delMenuIds.Contains(e.MenuId));
            var addDto = addMenuIds.Select(m => new EmployeeMenuRequestDto
            {
                Id = Guid.NewGuid().ToString(),
                MenuId = m,
                EmployeeId = employeeMenuRequestDto.EmployeeId,
                CreateUserId = employeeMenuRequestDto.CreateUserId,
                CreateUserName = employeeMenuRequestDto.CreateUserName,
            }).ToList();
            var delDto = delMenuIds.Select(m =>
             new EmployeeMenuRequestDto
             {
                 MenuId = m,
                 EmployeeId = employeeMenuRequestDto.EmployeeId,
                 IsDelete=true,
                 ModifyUserId = employeeMenuRequestDto.ModifyUserId,
                 ModifyUserName = employeeMenuRequestDto.ModifyUserName,
                 ModifyDate=DateTime.Now
             });
            return (addDto.ToList(), delDto.ToList());
        }
        #endregion
    }
}
