
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.ProxyGenerator;
using MicroService.Data.Common;
using MicroService.Data.Extensions;
using MicroService.Data.Validation;
using MicroService.Common.Models;
using MicroService.IApplication.Authority;
using MicroService.IApplication.Authority.Dto;
using MicroService.IModules.Authority;
using MicroService.IApplication.Authority.IDomainService;

namespace MicroService.Modules.Authority
 {
	/// <summary>
	/// Menu -Module实现
	/// </summary>
	[ModuleName("Menu")]
	public class MenuService: ProxyServiceBase, IMenuService
	{
	    private readonly IMenuAppService _menuAppService;
        private readonly IMenuDomainService _menuDomainService;
        private readonly IEmployeeDomainService _employeeDomainService;
        private readonly ApplicationEnginee _applicationEnginee;

        public MenuService(IMenuAppService menuAppService, IMenuDomainService menuDomainService,
            IEmployeeDomainService employeeDomainService)
        {
            _menuAppService = menuAppService;
            _menuDomainService = menuDomainService;
            _employeeDomainService = employeeDomainService;
            _applicationEnginee = new ApplicationEnginee();
        }
		/// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Create(MenuRequestDto input)
        {
			input.InitCreateRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _menuAppService.CreateAsync(input);
            });
            return resJson;
        }
         /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> BatchCreate(MenuBatchRequestDto input)
        {
			foreach (var menuRequestDto in input.MenuRequestList)
            {
                menuRequestDto.InitCreateRequest(input.Payload);
            }
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _menuAppService.BatchCreateAsync(input.MenuRequestList);
            });
            return resJson;
        }
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageData> GetPageList(MenuPageRequestDto input)
        { 
			input.InitRequest();
            return await _menuAppService.GetPageListAsync(input);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<MenuQueryDto> GetForModify(EntityQueryRequest input)
        {
			input.InitRequest();
            return await _menuAppService.GetForModifyAsync(input);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Modify(MenuRequestDto input)
        {
			input.InitModifyRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _menuAppService.ModifyAsync(input);
            });
            return resJson;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Remove(EntityRequest input)
        {
            input.InitModifyRequest();
            var resJson = await _menuDomainService.RemoveMenu(input);
            return resJson;
        }

        /// <summary>
        /// 保存机构菜单权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> SaveOrgMenu(OrganizationMenuRequestDto input)
        {
            input.InitCreateRequest();
            input.InitModifyRequest();
            return await _menuDomainService.SaveOrgMenu(input);
        }

        /// <summary>
        /// 保存角色菜单权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> SaveRoleMenu(RoleMenuRequestDto input)
        {
            input.InitCreateRequest();
            input.InitModifyRequest();
            return await _menuDomainService.SaveRoleMenu(input);
        }

        /// <summary>
        /// 保存人员菜单权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> SaveEmployeeMenu(EmployeeMenuRequestDto input)
        {
            input.InitCreateRequest();
            input.InitModifyRequest();
            return await _menuDomainService.SaveEmployeeMenu(input);
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IList<MenuTreeViewResponse>> GetMenuTree(MenuTreeQueryRequest input)
        {
            return await _menuAppService.GetMenuForTree(input);
        }

        /// <summary>
        /// 获取某机构菜单权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IList<PowerSelectViewResponse>> GetOrgMenu(OrganizationMenuRequestDto input)
        {
            return await _menuDomainService.GetOrgMenu(input);
        }
        /// <summary>
        /// 获取某角色菜单权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IList<PowerSelectViewResponse>> GetRoleMenu(RoleMenuRequestDto input)
        {
            return await _menuDomainService.GetRoleMenu(input);
        }

        /// <summary>
        /// 获取某成员菜单权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IList<PowerSelectViewResponse>> GetEmployeeMenu(EmployeeMenuRequestDto input)
        {
            return await _employeeDomainService.GetEmployeeMenu(input);
        }
        /// <summary>
        /// 获取当前用户所拥有菜单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IList<AntMenuTree>> GetCurrentMenuTree(EmployeeMenuRequestDto input)
        {
            input.InitRequest();
            return await _employeeDomainService.GetCurrentMenuTree(input);
        }
    }
}