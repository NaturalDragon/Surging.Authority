
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
	/// EmployeeElement -IModule接口
	/// </summary>
	[ServiceBundle("api/{Service}")]
	public interface IEmployeeElementService: IServiceKey
	{
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[HttpPost(true)]
        [Command(Strategy = StrategyType.Injection,Injection = Strategy.BaseStrategy,RequestCacheEnabled = true,InjectionNamespaces = new string[] { InjectionNamespace.JsonResponseNamespaces})]
	    [Authorization(AuthType = AuthorizationType.JWT)]
        Task<JsonResponse> Create(EmployeeElementRequestDto input);
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[HttpPost(true)]
        [Command(Strategy = StrategyType.Injection,Injection = Strategy.BaseStrategy,RequestCacheEnabled = true,InjectionNamespaces = new string[] { InjectionNamespace.JsonResponseNamespaces})]
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<JsonResponse> BatchCreate(EmployeeElementBatchRequestDto input);

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[HttpPost(true)]
        [Command(Strategy = StrategyType.Injection, Injection = Strategy.MessageStrategy, RequestCacheEnabled = true)]
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<PageData> GetPageList(EmployeeElementPageRequestDto input);
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[HttpPost(true)]
        [Command(Strategy = StrategyType.Injection, Injection = Strategy.MessageStrategy, RequestCacheEnabled = true)]
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<EmployeeElementQueryDto> GetForModify(EntityQueryRequest input);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[HttpPost(true)]
        [Command(Strategy = StrategyType.Injection,Injection = Strategy.BaseStrategy,RequestCacheEnabled = true,InjectionNamespaces = new string[] { InjectionNamespace.JsonResponseNamespaces})]
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<JsonResponse> Modify(EmployeeElementRequestDto input);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[HttpPost(true)]
        [Command(Strategy = StrategyType.Injection,Injection = Strategy.BaseStrategy,RequestCacheEnabled = true,InjectionNamespaces = new string[] { InjectionNamespace.JsonResponseNamespaces})]
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<JsonResponse> Remove(EntityRequest input);
	}
}