
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
	/// OrganizationElement -Module实现
	/// </summary>
	[ModuleName("OrganizationElement")]
	public class OrganizationElementService: ProxyServiceBase, IOrganizationElementService
	{
	    private readonly IOrganizationElementAppService _organizationElementAppService;
		private readonly ApplicationEnginee _applicationEnginee;
        public OrganizationElementService(IOrganizationElementAppService organizationElementAppService)
        {
            _organizationElementAppService = organizationElementAppService;
			_applicationEnginee = new ApplicationEnginee();
        }
		/// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Create(OrganizationElementRequestDto input)
        {
			input.InitCreateRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _organizationElementAppService.CreateAsync(input);
            });
            return resJson;
        }
         /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> BatchCreate(OrganizationElementBatchRequestDto input)
        {
			foreach (var organizationElementRequestDto in input.OrganizationElementRequestList)
            {
                organizationElementRequestDto.InitCreateRequest(input.Payload);
            }
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _organizationElementAppService.BatchCreateAsync(input.OrganizationElementRequestList);
            });
            return resJson;
        }
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageData> GetPageList(OrganizationElementPageRequestDto input)
        { 
			input.InitRequest();
            return await _organizationElementAppService.GetPageListAsync(input);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OrganizationElementQueryDto> GetForModify(EntityQueryRequest input)
        {
			input.InitRequest();
            return await _organizationElementAppService.GetForModifyAsync(input);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Modify(OrganizationElementRequestDto input)
        {
			input.InitModifyRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _organizationElementAppService.ModifyAsync(input);
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
               await _organizationElementAppService.RemoveAsync(input);
            });
            return resJson;
        }
	}
}