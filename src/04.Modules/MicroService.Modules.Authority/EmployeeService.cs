
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
    /// Employee -Module实现
    /// </summary>
    [ModuleName("Employee")]
    public class EmployeeService : ProxyServiceBase, IEmployeeService
    {
        private readonly IEmployeeAppService _employeeAppService;
        private readonly IEmployeeDomainService _employeeDomainService;
        private readonly ApplicationEnginee _applicationEnginee;
        public EmployeeService(IEmployeeAppService employeeAppService, IEmployeeDomainService employeeDomainService)
        {
            _employeeAppService = employeeAppService;
            _employeeDomainService = employeeDomainService;
            _applicationEnginee = new ApplicationEnginee();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Create(EmployeeRequestDto input)
        {
            input.InitCreateRequest();
            var resJson = await _employeeDomainService.SaveEmployee(input);
            return resJson;
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> BatchCreate(EmployeeBatchRequestDto input)
        {
            foreach (var employeeRequestDto in input.EmployeeRequestList)
            {
                employeeRequestDto.InitCreateRequest(input.Payload);
            }
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _employeeAppService.BatchCreateAsync(input.EmployeeRequestList);
            });
            return resJson;
        }
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageData> GetPageList(EmployeePageRequestDto input)
        {
            input.InitRequest();
            return await _employeeAppService.GetPageListAsync(input);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<EmployeeQueryDto> GetForModify(EntityQueryRequest input)
        {
            input.InitRequest();
            return await _employeeDomainService.GetEmployee(input);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Modify(EmployeeRequestDto input)
        {
            input.InitCreateRequest();
            input.InitModifyRequest();
            var resJson = await _employeeDomainService.SaveEmployee(input);
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
            return await _employeeDomainService.RemoveEmployee(input);
        }

        /// <summary>
        /// 获取成员信息分页
        /// </summary>
        /// <param name="employeePageRequestDto"></param>
        /// <returns></returns>
        public async Task<PageData> GetListPagedByOrgIdOrRoleId(EmployeePageRequestDto input)
        {
            return await _employeeDomainService.GetListPagedByOrgIdOrRoleId(input);
        }
        /// <summary>
        /// 获取成员信息分页
        /// </summary>
        /// <param name="employeePageRequestDto"></param>
        public async Task<PageData> GetListPagedOriginal(EmployeePageOriginalRequestDto input)
        {
            return await _employeeDomainService.GetListPagedOriginal(input);
        }
    }
}