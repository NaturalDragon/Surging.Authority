
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
	/// ModuleElement -Module实现
	/// </summary>
	[ModuleName("ModuleElement")]
	public class ModuleElementService: ProxyServiceBase, IModuleElementService
	{
	    private readonly IModuleElementAppService _moduleElementAppService;
        private readonly IModuleElementDomainService _moduleElementDomainService;
        private readonly IEmployeeDomainService _employeeDomainService;
        private readonly ApplicationEnginee _applicationEnginee;
        public ModuleElementService(IModuleElementAppService moduleElementAppService,
            IModuleElementDomainService moduleElementDomainService , IEmployeeDomainService employeeDomainService)
        {
            _moduleElementAppService = moduleElementAppService;
            _moduleElementDomainService = moduleElementDomainService;
            _employeeDomainService = employeeDomainService;
            _applicationEnginee = new ApplicationEnginee();
        }
		/// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Create(ModuleElementRequestDto input)
        {
			input.InitCreateRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _moduleElementAppService.CreateAsync(input);
            });
            return resJson;
        }
         /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> BatchCreate(ModuleElementBatchRequestDto input)
        {
			foreach (var moduleElementRequestDto in input.ModuleElementRequestList)
            {
                moduleElementRequestDto.InitCreateRequest(input.Payload);
            }
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _moduleElementAppService.BatchCreateAsync(input.ModuleElementRequestList);
            });
            return resJson;
        }
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageData> GetPageList(ModuleElementPageRequestDto input)
        { 
			input.InitRequest();
            return await _moduleElementAppService.GetPageListAsync(input);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ModuleElementQueryDto> GetForModify(EntityQueryRequest input)
        {
			input.InitRequest();
            return await _moduleElementAppService.GetForModifyAsync(input);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Modify(ModuleElementRequestDto input)
        {
			input.InitModifyRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _moduleElementAppService.ModifyAsync(input);
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
               await _moduleElementAppService.RemoveAsync(input);
            });
            return resJson;
        }

        /// <summary>
        /// 保存机构模块元素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> SaveOrganizationModuleElement(OrganizationElementRequestDto input)
        {
            input.InitCreateRequest();
            input.InitModifyRequest();
            return await _moduleElementDomainService.SaveOrganizationModuleElement(input);
        }
        /// <summary>
        /// 保存角色模块元素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> SaveRoleModuleElement(RoleElementRequestDto input)
        {
            input.InitCreateRequest();
            input.InitModifyRequest();
            return await _moduleElementDomainService.SaveRoleModuleElement(input);
        }
        /// <summary>
        /// 保存成员模块元素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> SaveEmployeeModuleElement(EmployeeElementRequestDto input)
        {
            input.InitCreateRequest();
            input.InitModifyRequest();
            return await _moduleElementDomainService.SaveEmployeeModuleElement(input);
        }
        /// <summary>
        /// 获取机构模块元素信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IList<PowerSelectViewResponse>> GetOrganizationModuleElement(OrganizationElementRequestDto input)
        {
            return await _moduleElementDomainService.GetOrganizationModuleElement(input);
        }
        /// <summary>
        /// 获取角色模块元素信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IList<PowerSelectViewResponse>> GetRoleModuleElement(RoleElementRequestDto input)
        {
            return await _moduleElementDomainService.GetRoleModuleElement(input);
        }
        /// <summary>
        /// 获取成员模块元素信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IList<PowerSelectViewResponse>> GetEmployeeModuleElement(EmployeeElementRequestDto input)
        {
            return await _employeeDomainService.GetEmployeeModuleElement(input);
        }
    }
}