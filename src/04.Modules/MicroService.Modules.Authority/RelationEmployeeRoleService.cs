
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
	/// RelationEmployeeRole -Module实现
	/// </summary>
	[ModuleName("RelationEmployeeRole")]
	public class RelationEmployeeRoleService: ProxyServiceBase, IRelationEmployeeRoleService
	{
	    private readonly IRelationEmployeeRoleAppService _relationEmployeeRoleAppService;
		private readonly ApplicationEnginee _applicationEnginee;
        public RelationEmployeeRoleService(IRelationEmployeeRoleAppService relationEmployeeRoleAppService)
        {
            _relationEmployeeRoleAppService = relationEmployeeRoleAppService;
			_applicationEnginee = new ApplicationEnginee();
        }
		/// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Create(RelationEmployeeRoleRequestDto input)
        {
			input.InitCreateRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _relationEmployeeRoleAppService.CreateAsync(input);
            });
            return resJson;
        }
         /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> BatchCreate(RelationEmployeeRoleBatchRequestDto input)
        {
			foreach (var relationEmployeeRoleRequestDto in input.RelationEmployeeRoleRequestList)
            {
                relationEmployeeRoleRequestDto.InitCreateRequest(input.Payload);
            }
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _relationEmployeeRoleAppService.BatchCreateAsync(input.RelationEmployeeRoleRequestList);
            });
            return resJson;
        }
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageData> GetPageList(RelationEmployeeRolePageRequestDto input)
        { 
			input.InitRequest();
            return await _relationEmployeeRoleAppService.GetPageListAsync(input);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<RelationEmployeeRoleQueryDto> GetForModify(EntityQueryRequest input)
        {
			input.InitRequest();
            return await _relationEmployeeRoleAppService.GetForModifyAsync(input);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Modify(RelationEmployeeRoleRequestDto input)
        {
			input.InitModifyRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _relationEmployeeRoleAppService.ModifyAsync(input);
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
               await _relationEmployeeRoleAppService.RemoveAsync(input);
            });
            return resJson;
        }
	}
}