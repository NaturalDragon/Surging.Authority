
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
using MicroService.IApplication.Authority.IDomainService;

namespace MicroService.Modules.Authority
 {
	/// <summary>
	/// Role -Module实现
	/// </summary>
	[ModuleName("Role")]
	public class RoleService: ProxyServiceBase, IRoleService
	{
	    private readonly IRoleAppService _roleAppService;
        private readonly IRoleDomainService _roleDomainService;
        private readonly ApplicationEnginee _applicationEnginee;
        public RoleService(IRoleAppService roleAppService, IRoleDomainService roleDomainService)
        {
            _roleAppService = roleAppService;
            _roleDomainService = roleDomainService;
            _applicationEnginee = new ApplicationEnginee();
        }
		/// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Create(RoleRequestDto input)
        {
			input.InitCreateRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _roleAppService.CreateAsync(input);
            });
            return resJson;
        }
         /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> BatchCreate(RoleBatchRequestDto input)
        {
			foreach (var roleRequestDto in input.RoleRequestList)
            {
                roleRequestDto.InitCreateRequest(input.Payload);
            }
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _roleAppService.BatchCreateAsync(input.RoleRequestList);
            });
            return resJson;
        }
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageData> GetPageList(RolePageRequestDto input)
        { 
			input.InitRequest();
            return await _roleAppService.GetPageListAsync(input);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<RoleQueryDto> GetForModify(EntityQueryRequest input)
        {
			input.InitRequest();
            return await _roleAppService.GetForModifyAsync(input);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Modify(RoleRequestDto input)
        {
			input.InitModifyRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _roleAppService.ModifyAsync(input);
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
            return await _roleDomainService.RemoveRole(input);
        }

        public async Task<IList<RoleTypeViewResponse>> GetRoleTypeList(EntityRequest input)
        {
            return await _roleAppService.GetRoleTypeList();
        }
    }
}