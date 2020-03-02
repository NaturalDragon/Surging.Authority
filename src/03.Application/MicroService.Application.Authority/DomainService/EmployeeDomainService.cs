using MicroService.Common.Core.Enums;
using MicroService.Common.Models;
using MicroService.Common.Security;
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
    public class EmployeeDomainService : IEmployeeDomainService
    {

        private readonly IOrganizationAppService _organizationAppService;
        private readonly IRoleAppService _roleAppService;
        private readonly IRelationOrganizationEmployeeAppService _relationOrganizationEmployeeAppService;
        private readonly IRelationEmployeeRoleAppService _relationEmployeeRoleAppService;
        private readonly IEmployeeAppService _employeeAppService;
        private readonly IOrganizationElementAppService _organizationElementAppService;
        private readonly IRoleElementAppService _roleElementAppService;
        private readonly IEmployeeElementAppService _employeeElementAppService;
        private readonly IOrganizationMenuAppService _organizationMenuAppService;
        private readonly IRoleMenuAppService _roleMenuAppService;
        private readonly IEmployeeMenuAppService _employeeMenuAppService;
        private readonly IModuleElementAppService _moduleElementAppService;
        private readonly IMenuAppService _menuAppService;
        private readonly IModuleAppService _moduleAppService;
        private readonly ApplicationEnginee _applicationEnginee;

        public EmployeeDomainService(IOrganizationAppService organizationAppService, IRoleAppService  roleAppService,
            IRelationOrganizationEmployeeAppService relationOrganizationEmployeeAppService, IRelationEmployeeRoleAppService relationEmployeeRoleAppService,
            IEmployeeAppService employeeAppService,IOrganizationElementAppService organizationElementAppService, IRoleElementAppService roleElementAppService,
            IEmployeeElementAppService employeeElementAppService, IOrganizationMenuAppService organizationMenuAppService,
            IRoleMenuAppService roleMenuAppService, IEmployeeMenuAppService employeeMenuAppService,
            IModuleElementAppService moduleElementAppService, IMenuAppService menuAppService, IModuleAppService moduleAppService)
        {
            _organizationAppService = organizationAppService;
            _roleAppService = roleAppService;
            _relationOrganizationEmployeeAppService = relationOrganizationEmployeeAppService;
            _relationEmployeeRoleAppService = relationEmployeeRoleAppService;
            _employeeAppService = employeeAppService;
            _organizationElementAppService = organizationElementAppService;
            _roleElementAppService = roleElementAppService;
            _employeeElementAppService = employeeElementAppService;
            _organizationMenuAppService = organizationMenuAppService;
            _roleMenuAppService = roleMenuAppService;
            _employeeMenuAppService = employeeMenuAppService;
            _moduleElementAppService = moduleElementAppService;
            _menuAppService = menuAppService;
            _moduleAppService = moduleAppService;
             _applicationEnginee = new ApplicationEnginee();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<LoginUser> Login(EmployeeRequestDto input)
        {
            var userAccount = await _employeeAppService.EmployeeLogin(input.UserId);
            if (userAccount == null)
            {
                var _loginUser = new LoginUser() { IsSucceed = false, Message = "用户名不存在!" };
                return await Task.FromResult(_loginUser);
            }
           var encryptPassword= AESCryption.EncryptText(input.Password, userAccount.Salt);
            if(!string.Equals(encryptPassword, userAccount.Password))
            {
                var _loginUser = new LoginUser() { IsSucceed = false, Message = "密码错误!" };
                return await Task.FromResult(_loginUser);
            }
            //var menuIds = await GetEmployeeMenuAsync(userAccount.Id);
            var elements= await GetEmployeeModuleElementAsync(userAccount.Id);
            var elementIds = elements.Select(e => e.Id).ToList();
            var elementDtos = await _moduleElementAppService.GetElementByIds(elementIds);
            var routePaths = elementDtos.Select(r => r.RoutePath).ToList();
            return await Task.FromResult(new LoginUser()
            {
                RoutePaths= routePaths,
                IsSucceed = true,
                Name = userAccount.Name,
                Account = userAccount.UserId,
                ExpireDateTime = DateTime.Now.AddHours(8),
                UserId = userAccount.Id,
            });
        }
        /// <summary>
        /// 获取成员当前菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IList<AntMenuTree>> GetCurrentMenuTree(EmployeeMenuRequestDto input)
        {
            var menus = await GetEmployeeMenuAsync(input.UserId);
            var menuIds = menus.Select(r => r.Id).ToList();
            var menuDtos = await _menuAppService.GetMenuByIdsAsync(menuIds);
            var moduleIds = menuDtos.Select(m => m.ModuleId).ToList();
            var moduleDtos = await _moduleAppService.GetModuleByIdsAsnyc(moduleIds);
            foreach (var item in menuDtos)
            {
                var module = moduleDtos.Where(m => m.Id == item.ModuleId).FirstOrDefault();
                item.Url = module?.Url;
            }
            var id = Guid.Empty.ToString();
            var mTree = GetAntTree(menuDtos, id);
            return mTree;

        }
        
        /// <summary>
        /// 获取成员信息分页
        /// </summary>
        /// <param name="employeePageRequestDto"></param>
        /// <returns></returns>
        public async Task<PageData> GetListPagedByOrgIdOrRoleId(EmployeePageRequestDto employeePageRequestDto)
        {
            employeePageRequestDto = await GetSubOrganizations(employeePageRequestDto);
            employeePageRequestDto = await GetRelationOranizationEmpIds(employeePageRequestDto);
            employeePageRequestDto = await GetRelationEmployeeRoleEmpIds(employeePageRequestDto);
            var pageData= await _employeeAppService.GetPageListAsync(employeePageRequestDto);
            var employeeDtos = pageData.Data as List<EmployeeQueryDto>;
            await GetRoleAndOrgName(employeeDtos);
            pageData.Data = employeeDtos;
            return pageData;

        }
        public async Task<PageData> GetListPagedOriginal(EmployeePageOriginalRequestDto employeePageOriginalRequestDto)
        {
            var pageData = await _employeeAppService.GetListPagedOriginalAsync(employeePageOriginalRequestDto);
            var employeeDtos = pageData.Data as List<EmployeeQueryDto>;
            await GetRoleAndOrgName(employeeDtos);
            pageData.Data = employeeDtos;
            return pageData;
        }
        /// <summary>
        /// 获取成员模块元素信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IList<PowerSelectViewResponse>> GetEmployeeModuleElement(EmployeeElementRequestDto input)
        {
            return await GetEmployeeModuleElementAsync(input.EmployeeId);
        }

       

        /// <summary>
        /// 获取某成员菜单权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IList<PowerSelectViewResponse>> GetEmployeeMenu(EmployeeMenuRequestDto input)
        {
            return await GetEmployeeMenuAsync(input.EmployeeId);
        }

        

        // <summary>
        /// 保存成员
        /// </summary>
        /// <param name="employeeRequestDto"></param>
        /// <returns></returns>
        public async Task<JsonResponse> SaveEmployee(EmployeeRequestDto employeeRequestDto)
        {
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await CreateOrModifyEmployee(employeeRequestDto);
                await CreateOrRemoveRelationOrganizationEmployee(employeeRequestDto);
                await CreateOrRemoveRelationRoleEmployee(employeeRequestDto);
            });
            return resJson;
        }
        /// <summary>
        /// 获取成员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<EmployeeQueryDto> GetEmployee(EntityQueryRequest input)
        {
            var employeeDto = await _employeeAppService.GetForModifyAsync(input);
            employeeDto.Password = "";
            employeeDto.Salt = "";
            employeeDto.OrganizationObjects = await GetOrganizationName(input);
            employeeDto.RoleObjects = await GetRoleName(input);
            return employeeDto;
        }

        /// <summary>
        /// 删除成员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> RemoveEmployee(EntityRequest entityRequest)
        {
            var relationOrgEmpIds =await _relationOrganizationEmployeeAppService.GetIdsByEmployeeIds(entityRequest.Ids);
            var relationRoleEmpIds = await _relationEmployeeRoleAppService.GetIdsByEmployeeIds(entityRequest.Ids);
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _employeeAppService.RemoveAsync(entityRequest);
                await _relationOrganizationEmployeeAppService.RemoveAsync(new EntityRequest()
                {
                    Ids = relationOrgEmpIds,
                    CreateDate = DateTime.Now,
                    CreateUserId = entityRequest.CreateUserId,
                    CreateUserName = entityRequest.CreateUserName
                });
                await _relationEmployeeRoleAppService.RemoveAsync(new EntityRequest()
                {
                    Ids = relationRoleEmpIds,
                    CreateDate = DateTime.Now,
                    CreateUserId = entityRequest.CreateUserId,
                    CreateUserName = entityRequest.CreateUserName
                });
            });
            return resJson;
        }
        #region private
        IList<AntMenuTree> GetAntTree(IList<MenuQueryDto> menuViewResponses, string parentId)
        {
            List<AntMenuTree> menuTrees = new List<AntMenuTree>();
            foreach (var item in menuViewResponses)
            {
                if (item.ParentId == parentId)
                {
                    AntMenuTree menuTree = new AntMenuTree();
                    menuTree.name = item.Name;
                    menuTree.path = item.Url;
                    menuTree.icon = item.Icon;
                    menuTree.component = item.Url;
                    menuTrees.Add(menuTree);
                    menuTree.children = GetAntTree(menuViewResponses, item.Id);
                }
            }
            return menuTrees;
        }
        private async Task<IList<PowerSelectViewResponse>> GetEmployeeMenuAsync(string employeeId)
        {
            var employeeMenuDtos = await _employeeMenuAppService.GetMenuByEmployeeIdAsync(employeeId);
            // return employeeMenuDtos.Select(o => new PowerSelectViewResponse { Id = o.MenuId }).ToList();
            var empMenuIds = employeeMenuDtos.Select(e => e.MenuId).ToList();
            //1.查询该人员所在机构下所拥有的菜单权限
            var relationOrgEmpViews = await _relationOrganizationEmployeeAppService.GetOrgIdsByEmployeeIds(new List<string> { employeeId });
            var orgIds = relationOrgEmpViews.Select(r => r.OrganizationId).ToList();
            var orgMenuViews = await _organizationMenuAppService.GetMenuByOrgIdsAsync(orgIds);
            var orgMenuIds = orgMenuViews.Select(o => o.MenuId).ToList();

            //2.查询该人员所在角色下所拥有的的菜单权限
            var relationRoleEmpViews = await _relationEmployeeRoleAppService.GetRoleIdsByEmployeeIds(new List<string> { employeeId });
            var roleIds = relationRoleEmpViews.Select(r => r.RoleId).ToList();
            var roleMenuViews = await _roleMenuAppService.GetMenuByRoleIdsAsync(roleIds);
            var roleMenuIds = roleMenuViews.Select(r => r.MenuId).ToList();


            //3.移除个人菜单中与机构、角色重复的
            empMenuIds.RemoveAll(e => orgMenuIds.Contains(e));
            empMenuIds.RemoveAll(e => roleMenuIds.Contains(e));

            //4.继承自机构的菜单权限，标记为禁止取消
            var powerSelectOrg = orgMenuIds.Select(o => new PowerSelectViewResponse { Id = o, Disabled = true });
            //5.继承自角色的菜单权限，标记为禁止取消
            var powerSelectRole = roleMenuIds.Select(r => new PowerSelectViewResponse { Id = r, Disabled = true });

            var powerSelectEmp = empMenuIds.Select(e => new PowerSelectViewResponse { Id = e });

            return powerSelectOrg.Concat(powerSelectRole).Concat(powerSelectEmp).ToList();
        }

        private async Task<IList<PowerSelectViewResponse>> GetEmployeeModuleElementAsync(string employeeId)
        {
            var empElementDtos = await _employeeElementAppService.GetElementByEmployeeIdAsync(employeeId);
            var empElementIds = empElementDtos.Select(o => o.ElementId).ToList();

            //1.查询该人员所在机构下所拥有的元素权限
            var relationOrgEmpViews = await _relationOrganizationEmployeeAppService.GetOrgIdsByEmployeeIds(new List<string> { employeeId });
            var orgIds = relationOrgEmpViews.Select(r => r.OrganizationId).ToList();
            var orgElementViews = await _organizationElementAppService.GetElementByOrganizationIdsAsync(orgIds);
            var orgElementIds = orgElementViews.Select(o => o.ElementId).ToList();

            //2.查询该人员所在角色下所拥有的的元素权限
            var relationRoleEmpViews = await _relationEmployeeRoleAppService.GetRoleIdsByEmployeeIds(new List<string> { employeeId });
            var roleIds = relationRoleEmpViews.Select(r => r.RoleId).ToList();
            var roleElementViews = await _roleElementAppService.GetElementByRoleIdsAsync(roleIds);
            var roleElementIds = roleElementViews.Select(r => r.ElementId).ToList();


            //3.移除个人元素中与机构、角色重复的
            empElementIds.RemoveAll(e => orgElementIds.Contains(e));
            empElementIds.RemoveAll(e => roleElementIds.Contains(e));

            //4.继承自机构的元素权限，标记为禁止取消
            var powerSelectOrg = orgElementIds.Select(o => new PowerSelectViewResponse { Id = o, Disabled = true });
            //5.继承自角色的元素权限，标记为禁止取消
            var powerSelectRole = roleElementIds.Select(r => new PowerSelectViewResponse { Id = r, Disabled = true });

            var powerSelectEmp = empElementIds.Select(e => new PowerSelectViewResponse { Id = e });

            return powerSelectOrg.Concat(powerSelectRole).Concat(powerSelectEmp).ToList();
        }
        private async Task GetRoleAndOrgName(IList<EmployeeQueryDto> employeeList)
        {
            var employeeIds = employeeList.Select(e => e.Id).ToList();

            IList<EmployeeQueryDto> empOrgs = await GetEmpOrgNames(employeeIds);

            IList<EmployeeQueryDto> empRoles = await GetEmpRoleNames(employeeIds);
            foreach (var employee in employeeList)
            {
                var empOrg = empOrgs.Where(e => e.Id == employee.Id).FirstOrDefault();
                var empRole = empRoles.Where(e => e.Id == employee.Id).FirstOrDefault();
                employee.Department = empOrg?.Department;
                employee.RoleName = empRole?.RoleName;
            }
        }

        private async Task<IList<EmployeeQueryDto>> GetEmpRoleNames(List<string> employeeIds)
        {
            var empRoles = await _relationEmployeeRoleAppService.GetRoleIdsByEmployeeIds(employeeIds);
            var groupEmpRoles = empRoles.GroupBy(r => r.EmployeeId)
                .Select(r => new EmployeeQueryDto { Id = r.Key, RoleIds = r.Select(rg => rg.RoleId).ToList() })
                .ToList();
            var roleIds = empRoles.Select(r => r.RoleId).ToList();
            var roleNames = await _roleAppService.GetRoleNameByIds(roleIds);
            foreach (var item in groupEmpRoles)
            {
                var roles = roleNames.Where(n =>item.RoleIds.Contains( n.Id)).Select(n => n.Name);
                item.RoleName = string.Join(";", roles);
            }

            return groupEmpRoles.ToList();
        }

        private async Task<IList<EmployeeQueryDto>> GetEmpOrgNames(List<string> employeeIds)
        {
            var empOrgs = await _relationOrganizationEmployeeAppService.GetOrgIdsByEmployeeIds(employeeIds);
           var groupEmpOrgs= empOrgs.GroupBy(o => o.EmployeeId)
                .Select(o=>new  EmployeeQueryDto{ Id=o.Key,
                    OrganizationIds=o.Select(og=>og.OrganizationId).ToList()}).ToList();
            var orgIds = empOrgs.Select(o => o.OrganizationId).ToList();
            var orgNames = await _organizationAppService.GetOrganizationNameByIds(orgIds);
            foreach (var item in groupEmpOrgs)
            {
                var orgs = orgNames.Where(n => item.OrganizationIds.Contains( n.Id)).Select(n => n.Name);
                
                item.Department = string.Join(";", orgs);
            }

            return groupEmpOrgs.ToList();
        }

        private async Task<IList<KeyValueJsonObject>> GetOrganizationName(EntityQueryRequest entityQueryRequest)
        {
            var empOrg= await _relationOrganizationEmployeeAppService.GetOrgIdsByEmployeeIds(new List<string>() { entityQueryRequest.Id });
            var dbOrgIds = empOrg.Select(o => o.OrganizationId).ToList();
            var orgDtos = await _organizationAppService.GetOrganizationNameByIds(dbOrgIds);
            return orgDtos.Select(o => new KeyValueJsonObject { Id=o.Id,Name=o.Name}).ToList();
        }
        private async Task<IList<KeyValueJsonObject>> GetRoleName(EntityQueryRequest entityQueryRequest)
        {
            var empRole= await _relationEmployeeRoleAppService.GetRoleIdsByEmployeeIds(new List<string>() { entityQueryRequest.Id });
            var dbRoleIds = empRole.Select(r => r.RoleId).ToList();
            var roleDtos = await _roleAppService.GetRoleNameByIds(dbRoleIds);
            return roleDtos.Select(o => new KeyValueJsonObject { Id = o.Id, Name = o.Name }).ToList();
        }
        private async Task CreateOrModifyEmployee(EmployeeRequestDto employeeRequestDto)
        {
            if (employeeRequestDto.OperationStatus == OperationModel.Create)
            {
                employeeRequestDto.Salt = Utils.Number(8);
                employeeRequestDto.Password = AESCryption.EncryptText(employeeRequestDto.Password, employeeRequestDto.Salt);
                await _employeeAppService.CreateAsync(employeeRequestDto);
            }
            else if (employeeRequestDto.OperationStatus == OperationModel.Modify)
            {
                await _employeeAppService.ModifyAsync(employeeRequestDto);
            }
        }
        private async Task CreateOrRemoveRelationOrganizationEmployee(EmployeeRequestDto employeeRequestDto)
        {
            var empOrg = await _relationOrganizationEmployeeAppService.GetOrgIdsByEmployeeIds(new List<string>() { employeeRequestDto.Id });
            var dbOrgIds = empOrg.Select(r => r.OrganizationId).ToList();
            var addOrgIds = employeeRequestDto.OrganizationIds.Except(dbOrgIds);
            var removeOrgIds = dbOrgIds.Except(employeeRequestDto.OrganizationIds);

            var addRelationOrgEmp = addOrgIds.Select(o => new RelationOrganizationEmployeeRequestDto
            {
                Id = Guid.NewGuid().ToString(),
                OrganizationId = o,
                EmployeeId = employeeRequestDto.Id,
                CreateUserId = employeeRequestDto.CreateUserId,
                CreateUserName = employeeRequestDto.CreateUserName
            });
            await _relationOrganizationEmployeeAppService.BatchCreateAsync(addRelationOrgEmp.ToList());

            var removeRelationOrgEmp = new RelationOrganizationEmployeeRemoveDto
            {
                EmployeeId = employeeRequestDto.Id,
                OrganizationIds = removeOrgIds.ToList(),
                ModifyUserId = employeeRequestDto.ModifyUserId,
                ModifyUserName = employeeRequestDto.ModifyUserName,
                ModifyDate = DateTime.Now
            };
            await _relationOrganizationEmployeeAppService.RemoveByEmployeeIdAndOrgIds(removeRelationOrgEmp);
        }

        private async Task CreateOrRemoveRelationRoleEmployee(EmployeeRequestDto employeeRequestDto)
        {
            var empRole = await _relationEmployeeRoleAppService.GetRoleIdsByEmployeeIds(new List<string>() { employeeRequestDto.Id });
            var dbRoleIds = empRole.Select(r => r.RoleId).ToList();
            var addRoleIds = employeeRequestDto.RoleIds.Except(dbRoleIds);
            var removeRoleIds = dbRoleIds.Except(employeeRequestDto.RoleIds);

            var addRelationRoleEmp = addRoleIds.Select(o => new RelationEmployeeRoleRequestDto
            {
                Id = Guid.NewGuid().ToString(),
                RoleId = o,
                EmployeeId = employeeRequestDto.Id,
                CreateUserId = employeeRequestDto.CreateUserId,
                CreateUserName = employeeRequestDto.CreateUserName
            });
            await _relationEmployeeRoleAppService.BatchCreateAsync(addRelationRoleEmp.ToList());

            var removeRelationRoleEmp = new RelationEmployeeRoleRemoveDto
            {
                EmployeeId = employeeRequestDto.Id,
                RoleIds = removeRoleIds.ToList(),
                ModifyUserId = employeeRequestDto.ModifyUserId,
                ModifyUserName = employeeRequestDto.ModifyUserName,
                ModifyDate = DateTime.Now
            };
            await _relationEmployeeRoleAppService.RemoveByEmployeeIdAndRoleIds(removeRelationRoleEmp);
        }

        private async Task<EmployeePageRequestDto> GetRelationOranizationEmpIds(EmployeePageRequestDto employeePageRequestDto)
        {
            if (employeePageRequestDto.OrganziationIds != null && employeePageRequestDto.OrganziationIds.Count > 0)
            {
              var empIds=  await _relationOrganizationEmployeeAppService.GetEmployeeIdsByOrgIds(employeePageRequestDto.OrganziationIds);
                if (empIds != null)
                {
                    employeePageRequestDto.Ids = employeePageRequestDto.Ids.Concat(empIds).ToList();
                }
            }
            return employeePageRequestDto;
        }

        private async Task<EmployeePageRequestDto> GetRelationEmployeeRoleEmpIds(EmployeePageRequestDto employeePageRequestDto)
        {
            if (!string.IsNullOrEmpty(employeePageRequestDto.RoleId))
            {
                var empIds = await _relationEmployeeRoleAppService.GetEmployeeIdsByRoleIds(new List<string>() { employeePageRequestDto.RoleId });
                if (empIds != null)
                {
                    employeePageRequestDto.Ids = employeePageRequestDto.Ids.Concat(empIds.ToList()).ToList();
                }
            }
            return employeePageRequestDto;
        }

        private async Task<EmployeePageRequestDto> GetSubOrganizations(EmployeePageRequestDto employeePageRequestDto)
        {
            if (string.IsNullOrEmpty(employeePageRequestDto.OrganizationId)) return employeePageRequestDto;
            var orgid = new List<string>();
            orgid.Add(employeePageRequestDto.OrganizationId);
            var orgIds = await GetSubOrganizations(orgid);
            orgid.AddRange(orgIds);
            employeePageRequestDto.OrganziationIds = orgid;

            return employeePageRequestDto;
        }

        /// <summary>
        /// 递归查找下级组织ID
        /// </summary>
        /// <param name="parentIds"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        private async Task<IList<string>> GetSubOrganizations(IList<string> parentIds)
        {

            var organizations = new List<string>();

            var children = await _organizationAppService
                .GetChildrenByParentIds(parentIds.ToList());

            if (children.Any())
            {
                var ids = children.Select(s => s.Id);
                organizations.AddRange(ids);

                var childIds = children.Select(m => m.Id).ToList();

                var result = await GetSubOrganizations(childIds);

                organizations.AddRange(result);
            }

            return organizations;
        }
        #endregion
    }
}
