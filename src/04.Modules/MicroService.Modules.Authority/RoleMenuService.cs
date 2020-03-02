
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
	/// RoleMenu -Module实现
	/// </summary>
	[ModuleName("RoleMenu")]
	public class RoleMenuService: ProxyServiceBase, IRoleMenuService
	{
	    private readonly IRoleMenuAppService _roleMenuAppService;
		private readonly ApplicationEnginee _applicationEnginee;
        public RoleMenuService(IRoleMenuAppService roleMenuAppService)
        {
            _roleMenuAppService = roleMenuAppService;
			_applicationEnginee = new ApplicationEnginee();
        }
		/// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Create(RoleMenuRequestDto input)
        {
			input.InitCreateRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _roleMenuAppService.CreateAsync(input);
            });
            return resJson;
        }
         /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> BatchCreate(RoleMenuBatchRequestDto input)
        {
			foreach (var roleMenuRequestDto in input.RoleMenuRequestList)
            {
                roleMenuRequestDto.InitCreateRequest(input.Payload);
            }
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _roleMenuAppService.BatchCreateAsync(input.RoleMenuRequestList);
            });
            return resJson;
        }
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageData> GetPageList(RoleMenuPageRequestDto input)
        { 
			input.InitRequest();
            return await _roleMenuAppService.GetPageListAsync(input);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<RoleMenuQueryDto> GetForModify(EntityQueryRequest input)
        {
			input.InitRequest();
            return await _roleMenuAppService.GetForModifyAsync(input);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Modify(RoleMenuRequestDto input)
        {
			input.InitModifyRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _roleMenuAppService.ModifyAsync(input);
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
               await _roleMenuAppService.RemoveAsync(input);
            });
            return resJson;
        }
	}
}