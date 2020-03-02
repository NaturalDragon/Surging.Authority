
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
using Newtonsoft.Json;

 namespace MicroService.Modules.Authority
 {
	/// <summary>
	/// OrganizationMenu -Module实现
	/// </summary>
	[ModuleName("OrganizationMenu")]
	public class OrganizationMenuService: ProxyServiceBase, IOrganizationMenuService
	{
	    private readonly IOrganizationMenuAppService _organizationMenuAppService;
		private readonly ApplicationEnginee _applicationEnginee;
        public OrganizationMenuService(IOrganizationMenuAppService organizationMenuAppService)
        {
            _organizationMenuAppService = organizationMenuAppService;
			_applicationEnginee = new ApplicationEnginee();
        }
		/// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Create(OrganizationMenuRequestDto input)
        {
			input.InitCreateRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _organizationMenuAppService.CreateAsync(input);
            });
            return resJson;
        }
         /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> BatchCreate(OrganizationMenuBatchRequestDto input)
        {
			foreach (var organizationMenuRequestDto in input.OrganizationMenuRequestList)
            {
                organizationMenuRequestDto.InitCreateRequest(input.Payload);
            }
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _organizationMenuAppService.BatchCreateAsync(input.OrganizationMenuRequestList);
            });
            return resJson;
        }
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageData> GetPageList(OrganizationMenuPageRequestDto input)
        { 
			input.InitRequest();
            return await _organizationMenuAppService.GetPageListAsync(input);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OrganizationMenuQueryDto> GetForModify(EntityQueryRequest input)
        {
			input.InitRequest();
            return await _organizationMenuAppService.GetForModifyAsync(input);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Modify(OrganizationMenuRequestDto input)
        {
			input.InitModifyRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _organizationMenuAppService.ModifyAsync(input);
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
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _organizationMenuAppService.RemoveAsync(input);
            });
            return resJson;
        }

     
    }
}