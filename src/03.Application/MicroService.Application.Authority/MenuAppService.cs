
using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MicroService.Data.Ext;
using MicroService.IApplication.Authority;
using MicroService.IRespository.Authority;
using MicroService.Entity.Authority;
using MicroService.IApplication.Authority.Dto;
using MicroService.Core.Data;
using MicroService.Data.Validation;
using MicroService.Data.Common;
using MicroService.Common.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicroService.Application.Authority.ValidatorsFilters;
using MicroService.Application.Authority.Extensions;

namespace MicroService.Application.Authority
 {
	/// <summary>
	/// Menu -applaction实现
	/// </summary>
	public class MenuAppService:IMenuAppService
	{
     
        private readonly IMenuRespository _menuRespository;
        private readonly IMapper _mapper;
        public MenuAppService(IMenuRespository menuRespository,
          IMapper mapper)
        {
            _menuRespository = menuRespository;
            _mapper = mapper;
        }
    
        /// <summary>
          /// 新增
          /// </summary>
          /// <param name="menuRequestDto"></param>
          /// <returns></returns>
        public async Task<bool> CreateAsync(MenuRequestDto menuRequestDto)
        {
         
            var menu = menuRequestDto.MapToCreateEntity<MenuRequestDto, Menu>();
            await MenuValidatorsFilter.DoValidationAsync(_menuRespository,menu, ValidatorTypeConstants.Create);
            return await _menuRespository.InsertAsync(menu);  
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="menuRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchCreateAsync(IList<MenuRequestDto> menuRequestDtos)
        {
            var entities =menuRequestDtos.MapToCreateEntities<MenuRequestDto, Menu>();
            await MenuValidatorsFilter.DoValidationAsync(_menuRespository,entities, ValidatorTypeConstants.Create);
            await _menuRespository.BatchInsertAsync(entities);
            return true;
         
        }

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="menuPageRequestDto"></param>
        /// <returns></returns>

        public async Task<PageData> GetPageListAsync( MenuPageRequestDto menuPageRequestDto)
        {
            var pageData = new PageData(menuPageRequestDto.PageIndex, menuPageRequestDto.PageSize);
            var list = await _menuRespository.WherePaged(pageData, e => e.IsDelete == false,
                e => e.CreateDate, false);
			 pageData.Data = list.MapToList<Menu, MenuQueryDto>().ToList();
            return pageData;

        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        public async Task<MenuQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest)
        {
            var entity = await _menuRespository.FirstOrDefaultAsync(e => e.Id == entityQueryRequest.Id);
            if (entity != null)
            {
                return entity.MapEntity<Menu, MenuQueryDto>();
            }
            return null;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="menuRequestDto"></param>
        /// <returns></returns>
        public async Task<bool> ModifyAsync(MenuRequestDto menuRequestDto)
        {
            var menu = await _menuRespository.FirstOrDefaultAsync(e => e.Id == menuRequestDto.Id);
            var entity = menuRequestDto.MapToModifyEntity<MenuRequestDto, Menu>(menu);
            await MenuValidatorsFilter.DoValidationAsync(_menuRespository,entity, ValidatorTypeConstants.Modify);
            return await _menuRespository.UpdateAsync(entity); 
        }

		/// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="menuRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchModifyAsync(IList<MenuRequestDto> menuRequestDtos)
        {
            var ids = menuRequestDtos.Select(m => m.Id).ToList();
            var menus = await _menuRespository.EntitiesByExpressionAsync(e => ids.Contains(e.Id));
            var entities = menuRequestDtos.MapToModifyEntities<MenuRequestDto,Menu>(menus.ToList());
            await MenuValidatorsFilter.DoValidationAsync(_menuRespository, entities, ValidatorTypeConstants.Create);
            await _menuRespository.BatchUpdateAsync(entities);
            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(EntityRequest entityRequest)
        {
            return await _menuRespository.UpdateAsync(entityRequest.Ids.ToArray(), async (e) =>
            {
                await Task.Run(() =>
               {
                   e.IsDelete = true;
                   e.ModifyUserId = entityRequest.ModifyUserId;
                   e.ModifyUserName = entityRequest.ModifyUserName;
                   e.ModifyDate = entityRequest.ModifyDate;
               });
            });
         
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="menuTreeQueryRequest"></param>
        /// <returns></returns>
      public async  Task<IList<MenuTreeViewResponse>> GetMenuForTree(MenuTreeQueryRequest menuTreeQueryRequest)
        {
            var menu = await _menuRespository.EntitiesByExpressionAsync(menuTreeQueryRequest.GetMenuTreeExpression()
                ,m=>new MenuQueryDto {  Name=m.Name,Id=m.Id,ParentId=m.ParentId,SortIndex=m.SortIndex},
                m=>m.SortIndex);

            var id = Guid.Empty.ToString();
            var result = GetTree(menu.ToList(), id);
            return result.OrderBy(r=>r.sortIndex).ToList();
        }
        /// <summary>
        /// 根据ids获取菜单信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
       public async Task<IList<MenuQueryDto>> GetMenuByIdsAsync(IList<string> ids)
        {
            var menu = await _menuRespository.EntitiesByExpressionAsync(c => ids.Contains(c.Id) && c.IsDelete == false
                , m => new MenuQueryDto
                {
                    Name = m.Name,
                    Id = m.Id,
                    Icon=m.Icon,
                    ModuleId=m.ModuleId,
                    ParentId = m.ParentId,
                },m=>m.SortIndex);
            return menu.ToList();
        }
        #region private
        private List<MenuTreeViewResponse> GetTree(List<MenuQueryDto> menuQueryDtos, string parentId)
        {
            List<MenuTreeViewResponse> menuTrees = new List<MenuTreeViewResponse>();
            foreach (var item in menuQueryDtos)
            {
                if (item.ParentId == parentId)
                {
                    MenuTreeViewResponse menuTree = new MenuTreeViewResponse();
                    menuTree.key = item.Id;
                    menuTree.value = item.Id;
                    menuTree.title = item.Name;
                    menuTree.path = item.Url;
                    menuTree.sortIndex = item.SortIndex;
                    menuTrees.Add(menuTree);
                    menuTree.children = GetTree(menuQueryDtos, item.Id);
                }
            }
            return menuTrees;
        }
        #endregion

    }
}