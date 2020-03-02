
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
	/// Employee -applaction实现
	/// </summary>
	public interface IEmployeeAppService: IDependency
	{
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="employeeRequestDto"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(EmployeeRequestDto employeeRequestDto);
		/// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="employeeRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchCreateAsync(IList<EmployeeRequestDto> employeeRequestDtos);
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="employeePageRequestDto"></param>
        /// <returns></returns>
        Task<PageData> GetPageListAsync(EmployeePageRequestDto employeePageRequestDto);

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="employeePageRequestDto"></param>
        /// <returns></returns>
        Task<PageData> GetListPagedOriginalAsync(EmployeePageOriginalRequestDto  employeePageOriginalRequestDto);
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        Task<EmployeeQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest);
		/// <summary>
        /// 修改
        /// </summary>
        /// <param name="employeeRequestDto"></param>
        /// <returns></returns>
        Task<bool> ModifyAsync(EmployeeRequestDto employeeRequestDto);
		/// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="employeeRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchModifyAsync(IList<EmployeeRequestDto> employeeRequestDtos);
		/// <summary>
        /// 删除
        /// </summary>
        /// <param name="entityRequest"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(EntityRequest entityRequest);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<EmployeeQueryDto> EmployeeLogin(string userId);

	}
}