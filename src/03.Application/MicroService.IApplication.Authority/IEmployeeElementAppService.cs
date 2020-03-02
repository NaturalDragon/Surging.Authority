
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MicroService.Core;
using MicroService.Data.Common;
using MicroService.Data.Validation;
using MicroService.Common.Models;
using MicroService.Entity.Authority;
using MicroService.IApplication.Authority.Dto;

 namespace MicroService.IApplication.Authority
 {
	/// <summary>
	/// EmployeeElement -applaction实现
	/// </summary>
	public interface IEmployeeElementAppService: IDependency
	{
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="employeeElementRequestDto"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(EmployeeElementRequestDto employeeElementRequestDto);
		/// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="employeeElementRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchCreateAsync(IList<EmployeeElementRequestDto> employeeElementRequestDtos);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="employeeElementRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchRemoveAsync(IList<EmployeeElementRequestDto> employeeElementRequestDtos);
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="employeeElementPageRequestDto"></param>
        /// <returns></returns>
        Task<PageData> GetPageListAsync(EmployeeElementPageRequestDto employeeElementPageRequestDto);
		/// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        Task<EmployeeElementQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest);
		/// <summary>
        /// 修改
        /// </summary>
        /// <param name="employeeElementRequestDto"></param>
        /// <returns></returns>
        Task<bool> ModifyAsync(EmployeeElementRequestDto employeeElementRequestDto);
		/// <summary>
        /// 删除
        /// </summary>
        /// <param name="entityRequest"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(EntityRequest entityRequest);

        /// <summary>
        /// 根据remployeeId获取模块元素
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<IList<EmployeeElementQueryDto>> GetElementByEmployeeIdAsync(string employeeId);

        /// <summary>
        /// 根据模块元素Id获取主键
        /// </summary>
        /// <param name="moduleElementIds"></param>
        /// <returns></returns>
        Task<IList<string>> GetIdsByModuleElementIdsAsync(IList<string> moduleElementIds);
    }
}