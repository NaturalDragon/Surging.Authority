
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
	/// EmployeeMenu -applaction实现
	/// </summary>
	public interface IEmployeeMenuAppService: IDependency
	{
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="employeeMenuRequestDto"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(EmployeeMenuRequestDto employeeMenuRequestDto);
		/// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="employeeMenuRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchCreateAsync(IList<EmployeeMenuRequestDto> employeeMenuRequestDtos);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="employeeMenuRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchRemoveAsync(IList<EmployeeMenuRequestDto> employeeMenuRequestDtos);
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="employeeMenuPageRequestDto"></param>
        /// <returns></returns>
        Task<PageData> GetPageListAsync(EmployeeMenuPageRequestDto employeeMenuPageRequestDto);
		/// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        Task<EmployeeMenuQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest);
		/// <summary>
        /// 修改
        /// </summary>
        /// <param name="employeeMenuRequestDto"></param>
        /// <returns></returns>
        Task<bool> ModifyAsync(EmployeeMenuRequestDto employeeMenuRequestDto);
		/// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="employeeMenuRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchModifyAsync(IList<EmployeeMenuRequestDto> employeeMenuRequestDtos);
		/// <summary>
        /// 删除
        /// </summary>
        /// <param name="entityRequest"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(EntityRequest entityRequest);

        /// <summary>
        /// 根据employeeId获取菜单
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<IList<EmployeeMenuQueryDto>> GetMenuByEmployeeIdAsync(string employeeId);

        /// <summary>
        /// 根据菜单Id获取主键
        /// </summary>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        Task<IList<string>> GetIdsByMenuIdsAsync(IList<string> menuIds);
    }
}