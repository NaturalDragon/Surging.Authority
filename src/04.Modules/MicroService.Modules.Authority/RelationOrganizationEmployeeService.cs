
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
	/// RelationOrganizationEmployee -Module实现
	/// </summary>
	[ModuleName("RelationOrganizationEmployee")]
	public class RelationOrganizationEmployeeService: ProxyServiceBase, IRelationOrganizationEmployeeService
	{
	    private readonly IRelationOrganizationEmployeeAppService _relationOrganizationEmployeeAppService;
		private readonly ApplicationEnginee _applicationEnginee;
        public RelationOrganizationEmployeeService(IRelationOrganizationEmployeeAppService relationOrganizationEmployeeAppService)
        {
            _relationOrganizationEmployeeAppService = relationOrganizationEmployeeAppService;
			_applicationEnginee = new ApplicationEnginee();
        }
		/// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Create(RelationOrganizationEmployeeRequestDto input)
        {
			input.InitCreateRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _relationOrganizationEmployeeAppService.CreateAsync(input);
            });
            return resJson;
        }
         /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> BatchCreate(RelationOrganizationEmployeeBatchRequestDto input)
        {
			foreach (var relationOrganizationEmployeeRequestDto in input.RelationOrganizationEmployeeRequestList)
            {
                relationOrganizationEmployeeRequestDto.InitCreateRequest(input.Payload);
            }
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _relationOrganizationEmployeeAppService.BatchCreateAsync(input.RelationOrganizationEmployeeRequestList);
            });
            return resJson;
        }
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageData> GetPageList(RelationOrganizationEmployeePageRequestDto input)
        { 
			input.InitRequest();
            return await _relationOrganizationEmployeeAppService.GetPageListAsync(input);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<RelationOrganizationEmployeeQueryDto> GetForModify(EntityQueryRequest input)
        {
			input.InitRequest();
            return await _relationOrganizationEmployeeAppService.GetForModifyAsync(input);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Modify(RelationOrganizationEmployeeRequestDto input)
        {
			input.InitModifyRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _relationOrganizationEmployeeAppService.ModifyAsync(input);
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
               await _relationOrganizationEmployeeAppService.RemoveAsync(input);
            });
            return resJson;
        }
	}
}