
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
	/// Menu -applaction实现
	/// </summary>
	public interface IMenuAppService: IDependency
	{
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="menuRequestDto"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(MenuRequestDto menuRequestDto);
		/// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="menuRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchCreateAsync(IList<MenuRequestDto> menuRequestDtos);
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="menuPageRequestDto"></param>
        /// <returns></returns>
        Task<PageData> GetPageListAsync(MenuPageRequestDto menuPageRequestDto);
		/// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        Task<MenuQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest);
		/// <summary>
        /// 修改
        /// </summary>
        /// <param name="menuRequestDto"></param>
        /// <returns></returns>
        Task<bool> ModifyAsync(MenuRequestDto menuRequestDto);
		/// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="menuRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchModifyAsync(IList<MenuRequestDto> menuRequestDtos);
		/// <summary>
        /// 删除
        /// </summary>
        /// <param name="entityRequest"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(EntityRequest entityRequest);

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="menuTreeQueryRequest"></param>
        /// <returns></returns>
        Task<IList<MenuTreeViewResponse>> GetMenuForTree(MenuTreeQueryRequest menuTreeQueryRequest);


        /// <summary>
        /// 根据ids获取菜单信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IList<MenuQueryDto>> GetMenuByIdsAsync(IList<string> ids);
    }
}