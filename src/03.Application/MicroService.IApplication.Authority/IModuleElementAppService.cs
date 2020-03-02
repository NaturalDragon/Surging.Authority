
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
	/// ModuleElement -applaction实现
	/// </summary>
	public interface IModuleElementAppService: IDependency
	{
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="moduleElementRequestDto"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(ModuleElementRequestDto moduleElementRequestDto);
		/// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="moduleElementRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchCreateAsync(IList<ModuleElementRequestDto> moduleElementRequestDtos);
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="moduleElementPageRequestDto"></param>
        /// <returns></returns>
        Task<PageData> GetPageListAsync(ModuleElementPageRequestDto moduleElementPageRequestDto);
		/// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        Task<ModuleElementQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest);
		/// <summary>
        /// 修改
        /// </summary>
        /// <param name="moduleElementRequestDto"></param>
        /// <returns></returns>
        Task<bool> ModifyAsync(ModuleElementRequestDto moduleElementRequestDto);
		/// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="moduleElementRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchModifyAsync(IList<ModuleElementRequestDto> moduleElementRequestDtos);
		/// <summary>
        /// 删除
        /// </summary>
        /// <param name="entityRequest"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(EntityRequest entityRequest);

        /// <summary>
        /// 根据模块Id获取其元素集合
        /// </summary>
        /// <param name="moduleIds"></param>
        /// <returns></returns>
        Task<IList<ModuleElementQueryDto>> GetElementByModuleIds(IList<string> moduleIds);

        /// <summary>
        /// 根据ids获取其元素集合
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IList<ModuleElementQueryDto>> GetElementByIds(IList<string> ids);

        /// <summary>
        /// 根据模块Id获取主键
        /// </summary>
        /// <param name="moduleIds"></param>
        /// <returns></returns>
        Task<IList<string>> GetIdsByModuleIdsAsync(IList<string> moduleIds);
    }
}