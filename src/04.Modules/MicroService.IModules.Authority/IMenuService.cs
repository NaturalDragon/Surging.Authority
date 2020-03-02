using MicroService.Common.Models;
using MicroService.Data.Common;
using MicroService.Data.Validation;
using MicroService.FuseStrategy.Injection;
using MicroService.IApplication.Authority.Dto;
using Surging.Core.CPlatform.Filters.Implementation;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using Surging.Core.CPlatform.Support;
using Surging.Core.CPlatform.Support.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroService.IModules.Authority
{
    /// <summary>
    /// Menu -IModule接口
    /// </summary>
    [ServiceBundle("api/{Service}")]
	public interface IMenuService: IServiceKey
	{
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[HttpPost(true)]
        [Command(Strategy = StrategyType.Injection,Injection = Strategy.BaseStrategy,RequestCacheEnabled = true,InjectionNamespaces = new string[] { InjectionNamespace.JsonResponseNamespaces})]
	    [Authorization(AuthType = AuthorizationType.JWT)]
        Task<JsonResponse> Create(MenuRequestDto input);
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[HttpPost(true)]
        [Command(Strategy = StrategyType.Injection,Injection = Strategy.BaseStrategy,RequestCacheEnabled = true,InjectionNamespaces = new string[] { InjectionNamespace.JsonResponseNamespaces})]
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<JsonResponse> BatchCreate(MenuBatchRequestDto input);

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[HttpPost(true)]
        [Command(Strategy = StrategyType.Injection, Injection = Strategy.MessageStrategy, RequestCacheEnabled = true)]
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<PageData> GetPageList(MenuPageRequestDto input);
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[HttpPost(true)]
        [Command(Strategy = StrategyType.Injection, Injection = Strategy.MessageStrategy, RequestCacheEnabled = true)]
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<MenuQueryDto> GetForModify(EntityQueryRequest input);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[HttpPost(true)]
        [Command(Strategy = StrategyType.Injection,Injection = Strategy.BaseStrategy,RequestCacheEnabled = true,InjectionNamespaces = new string[] { InjectionNamespace.JsonResponseNamespaces})]
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<JsonResponse> Modify(MenuRequestDto input);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[HttpPost(true)]
        [Command(Strategy = StrategyType.Injection,Injection = Strategy.BaseStrategy,RequestCacheEnabled = true,InjectionNamespaces = new string[] { InjectionNamespace.JsonResponseNamespaces})]
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<JsonResponse> Remove(EntityRequest input);


        /// <summary>
        /// 保存机构菜单权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost(true)]
        [Command(Strategy = StrategyType.Injection, Injection = Strategy.BaseStrategy, RequestCacheEnabled = true, InjectionNamespaces = new string[] { InjectionNamespace.JsonResponseNamespaces })]
        [Authorization(AuthType = AuthorizationType.JWT)]
        Task<JsonResponse> SaveOrgMenu(OrganizationMenuRequestDto input);

        /// <summary>
        /// 保存角色菜单权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost(true)]
        [Command(Strategy = StrategyType.Injection, Injection = Strategy.BaseStrategy, RequestCacheEnabled = true, InjectionNamespaces = new string[] { InjectionNamespace.JsonResponseNamespaces })]
        [Authorization(AuthType = AuthorizationType.JWT)]
        Task<JsonResponse> SaveRoleMenu(RoleMenuRequestDto input);

        /// <summary>
        /// 保存人员菜单权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost(true)]
        [Command(Strategy = StrategyType.Injection, Injection = Strategy.BaseStrategy, RequestCacheEnabled = true, InjectionNamespaces = new string[] { InjectionNamespace.JsonResponseNamespaces })]
        [Authorization(AuthType = AuthorizationType.JWT)]
        Task<JsonResponse> SaveEmployeeMenu(EmployeeMenuRequestDto input);

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost(true)]
        [Command(Strategy = StrategyType.Injection, Injection = Strategy.MessageStrategy, RequestCacheEnabled = true)]
        [Authorization(AuthType = AuthorizationType.JWT)]
        Task<IList<MenuTreeViewResponse>> GetMenuTree(MenuTreeQueryRequest input);


        /// <summary>
        /// 获取某机构菜单权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost(true)]
        [Command(Strategy = StrategyType.Injection, Injection = Strategy.MessageStrategy, RequestCacheEnabled = true)]
        [Authorization(AuthType = AuthorizationType.JWT)]
        Task<IList<PowerSelectViewResponse>> GetOrgMenu(OrganizationMenuRequestDto input);

        /// <summary>
        /// 获取某角色菜单权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost(true)]
        [Command(Strategy = StrategyType.Injection, Injection = Strategy.MessageStrategy, RequestCacheEnabled = true)]
        [Authorization(AuthType = AuthorizationType.JWT)]
        Task<IList<PowerSelectViewResponse>> GetRoleMenu(RoleMenuRequestDto input);

        /// <summary>
        /// 获取某成员菜单权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost(true)]
        [Command(Strategy = StrategyType.Injection, Injection = Strategy.MessageStrategy, RequestCacheEnabled = true)]
        [Authorization(AuthType = AuthorizationType.JWT)]
        Task<IList<PowerSelectViewResponse>> GetEmployeeMenu(EmployeeMenuRequestDto input);

        /// <summary>
        /// 获取当前用户所拥有菜单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost(true)]
        [Command(Strategy = StrategyType.Injection, Injection = Strategy.MessageStrategy, RequestCacheEnabled = true)]
        [Authorization(AuthType = AuthorizationType.JWT)]
        Task<IList<AntMenuTree>> GetCurrentMenuTree(EmployeeMenuRequestDto input);

    }
}