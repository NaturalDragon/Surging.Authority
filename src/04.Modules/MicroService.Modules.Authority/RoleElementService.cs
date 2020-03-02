
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
	/// RoleElement -Module实现
	/// </summary>
	[ModuleName("RoleElement")]
	public class RoleElementService: ProxyServiceBase, IRoleElementService
	{
	    private readonly IRoleElementAppService _roleElementAppService;
		private readonly ApplicationEnginee _applicationEnginee;
        public RoleElementService(IRoleElementAppService roleElementAppService)
        {
            _roleElementAppService = roleElementAppService;
			_applicationEnginee = new ApplicationEnginee();
        }
		/// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Create(RoleElementRequestDto input)
        {
			input.InitCreateRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _roleElementAppService.CreateAsync(input);
            });
            return resJson;
        }
         /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> BatchCreate(RoleElementBatchRequestDto input)
        {
			foreach (var roleElementRequestDto in input.RoleElementRequestList)
            {
                roleElementRequestDto.InitCreateRequest(input.Payload);
            }
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _roleElementAppService.BatchCreateAsync(input.RoleElementRequestList);
            });
            return resJson;
        }
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageData> GetPageList(RoleElementPageRequestDto input)
        { 
			input.InitRequest();
            return await _roleElementAppService.GetPageListAsync(input);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<RoleElementQueryDto> GetForModify(EntityQueryRequest input)
        {
			input.InitRequest();
            return await _roleElementAppService.GetForModifyAsync(input);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Modify(RoleElementRequestDto input)
        {
			input.InitModifyRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _roleElementAppService.ModifyAsync(input);
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
               await _roleElementAppService.RemoveAsync(input);
            });
            return resJson;
        }
	}
}