
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
	/// Module -applaction实现
	/// </summary>
	public interface IModuleAppService: IDependency
	{
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="moduleRequestDto"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(ModuleRequestDto moduleRequestDto);
		/// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="moduleRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchCreateAsync(IList<ModuleRequestDto> moduleRequestDtos);
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="modulePageRequestDto"></param>
        /// <returns></returns>
        Task<PageData> GetPageListAsync(ModulePageRequestDto modulePageRequestDto);
		/// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        Task<ModuleQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest);
		/// <summary>
        /// 修改
        /// </summary>
        /// <param name="moduleRequestDto"></param>
        /// <returns></returns>
        Task<bool> ModifyAsync(ModuleRequestDto moduleRequestDto);
		/// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="moduleRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchModifyAsync(IList<ModuleRequestDto> moduleRequestDtos);
		/// <summary>
        /// 删除
        /// </summary>
        /// <param name="entityRequest"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(EntityRequest entityRequest);

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <returns></returns>
        Task<List<ModuleQueryDto>> GetAll();

        /// <summary>
        /// 根据ids获取模块
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<List<ModuleQueryDto>> GetModuleByIdsAsnyc(IList<string> ids);
    }
}