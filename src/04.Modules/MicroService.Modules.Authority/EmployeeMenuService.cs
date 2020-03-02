
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
	/// EmployeeMenu -Module实现
	/// </summary>
	[ModuleName("EmployeeMenu")]
	public class EmployeeMenuService: ProxyServiceBase, IEmployeeMenuService
	{
	    private readonly IEmployeeMenuAppService _employeeMenuAppService;
		private readonly ApplicationEnginee _applicationEnginee;
        public EmployeeMenuService(IEmployeeMenuAppService employeeMenuAppService)
        {
            _employeeMenuAppService = employeeMenuAppService;
			_applicationEnginee = new ApplicationEnginee();
        }
		/// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Create(EmployeeMenuRequestDto input)
        {
			input.InitCreateRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _employeeMenuAppService.CreateAsync(input);
            });
            return resJson;
        }
         /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> BatchCreate(EmployeeMenuBatchRequestDto input)
        {
			foreach (var employeeMenuRequestDto in input.EmployeeMenuRequestList)
            {
                employeeMenuRequestDto.InitCreateRequest(input.Payload);
            }
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _employeeMenuAppService.BatchCreateAsync(input.EmployeeMenuRequestList);
            });
            return resJson;
        }
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageData> GetPageList(EmployeeMenuPageRequestDto input)
        { 
			input.InitRequest();
            return await _employeeMenuAppService.GetPageListAsync(input);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<EmployeeMenuQueryDto> GetForModify(EntityQueryRequest input)
        {
			input.InitRequest();
            return await _employeeMenuAppService.GetForModifyAsync(input);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Modify(EmployeeMenuRequestDto input)
        {
			input.InitModifyRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _employeeMenuAppService.ModifyAsync(input);
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
               await _employeeMenuAppService.RemoveAsync(input);
            });
            return resJson;
        }
	}
}