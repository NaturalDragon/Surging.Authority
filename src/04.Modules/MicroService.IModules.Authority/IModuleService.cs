
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MicroService.Core;
using MicroService.Data.Common;
using MicroService.Common.Models;
using MicroService.Data.Validation;
using MicroService.IApplication.Authority.Dto;
using MicroService.FuseStrategy.Injection;
using Surging.Core.CPlatform.Filters.Implementation;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using Surging.Core.CPlatform.Support.Attributes;
using Surging.Core.CPlatform.Support;
using Surging.Core.System.Intercept;
using Surging.Core.Caching;
 namespace MicroService.IModules.Authority
 {
	/// <summary>
	/// Module -IModule接口
	/// </summary>
	[ServiceBundle("api/{Service}")]
	public interface IModuleService: IServiceKey
	{
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[HttpPost(true)]
        [Command(Strategy = StrategyType.Injection,Injection = Strategy.BaseStrategy,RequestCacheEnabled = true,InjectionNamespaces = new string[] { InjectionNamespace.JsonResponseNamespaces})]
	    [Authorization(AuthType = AuthorizationType.JWT)]
        Task<JsonResponse> Create(ModuleRequestDto input);

       

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[HttpPost(true)]
        [Command(Strategy = StrategyType.Injection,Injection = Strategy.BaseStrategy,RequestCacheEnabled = true,InjectionNamespaces = new string[] { InjectionNamespace.JsonResponseNamespaces})]
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<JsonResponse> BatchCreate(ModuleBatchRequestDto input);

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[HttpPost(true)]
        [Command(Strategy = StrategyType.Injection, Injection = Strategy.MessageStrategy, RequestCacheEnabled = true)]
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<PageData> GetPageList(ModulePageRequestDto input);
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[HttpPost(true)]
        [Command(Strategy = StrategyType.Injection, Injection = Strategy.MessageStrategy, RequestCacheEnabled = true)]
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<ModuleQueryDto> GetForModify(EntityQueryRequest input);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[HttpPost(true)]
        [Command(Strategy = StrategyType.Injection,Injection = Strategy.BaseStrategy,RequestCacheEnabled = true,InjectionNamespaces = new string[] { InjectionNamespace.JsonResponseNamespaces})]
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<JsonResponse> Modify(ModuleRequestDto input);
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
        /// 获取全部
        /// </summary>
        /// <returns></returns>
        [HttpPost(true)]
        [Command(Strategy = StrategyType.Injection, Injection = Strategy.MessageStrategy, RequestCacheEnabled = true)]
        [Authorization(AuthType = AuthorizationType.JWT)]
        Task<List<ModuleQueryDto>> GetAll(EntityRequest input);

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <returns></returns>
        [HttpPost(true)]
        [Command(Strategy = StrategyType.Injection, Injection = Strategy.MessageStrategy, RequestCacheEnabled = true)]
        [Authorization(AuthType = AuthorizationType.JWT)]
        Task<IList<ModuleQueryDto>> GetModulesWithElements(EntityRequest input);
    }
}