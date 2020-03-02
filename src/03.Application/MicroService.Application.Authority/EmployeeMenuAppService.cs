
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
 namespace MicroService.Application.Authority
 {
	/// <summary>
	/// EmployeeMenu -applaction实现
	/// </summary>
	public class EmployeeMenuAppService:IEmployeeMenuAppService
	{
     
        private readonly IEmployeeMenuRespository _employeeMenuRespository;
        private readonly IMapper _mapper;
        public EmployeeMenuAppService(IEmployeeMenuRespository employeeMenuRespository,
          IMapper mapper)
        {
            _employeeMenuRespository = employeeMenuRespository;
            _mapper = mapper;
        }
    
        /// <summary>
          /// 新增
          /// </summary>
          /// <param name="employeeMenuRequestDto"></param>
          /// <returns></returns>
        public async Task<bool> CreateAsync(EmployeeMenuRequestDto employeeMenuRequestDto)
        {
         
            var employeeMenu = employeeMenuRequestDto.MapToCreateEntity<EmployeeMenuRequestDto, EmployeeMenu>();
            await EmployeeMenuValidatorsFilter.DoValidationAsync(_employeeMenuRespository,employeeMenu, ValidatorTypeConstants.Create);
            return await _employeeMenuRespository.InsertAsync(employeeMenu);  
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="employeeMenuRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchCreateAsync(IList<EmployeeMenuRequestDto> employeeMenuRequestDtos)
        {
            var entities =employeeMenuRequestDtos.MapToCreateEntities<EmployeeMenuRequestDto, EmployeeMenu>();
            await EmployeeMenuValidatorsFilter.DoValidationAsync(_employeeMenuRespository,entities, ValidatorTypeConstants.Create);
            await _employeeMenuRespository.BatchInsertAsync(entities);
            return true;
         
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="employeeMenuRequestDtos"></param>
        /// <returns></returns>
       public async Task<bool> BatchRemoveAsync(IList<EmployeeMenuRequestDto> employeeMenuRequestDtos)
        {
            var empIds = employeeMenuRequestDtos.Select(m => m.EmployeeId).ToList();
            var menuIds = employeeMenuRequestDtos.Select(m => m.MenuId).ToList();
            var employeeMenus = await _employeeMenuRespository.EntitiesByExpressionAsync(e => empIds.Contains(e.EmployeeId)
            &&menuIds.Contains(e.MenuId)&&e.IsDelete==false);
            var entities = employeeMenuRequestDtos.MapToModifyEntities<EmployeeMenuRequestDto, EmployeeMenu>(employeeMenus.ToList());
            await EmployeeMenuValidatorsFilter.DoValidationAsync(_employeeMenuRespository, entities, ValidatorTypeConstants.Create);
            await _employeeMenuRespository.BatchUpdateAsync(entities);
            return true;
        }
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="employeeMenuPageRequestDto"></param>
        /// <returns></returns>

        public async Task<PageData> GetPageListAsync( EmployeeMenuPageRequestDto employeeMenuPageRequestDto)
        {
            var pageData = new PageData(employeeMenuPageRequestDto.PageIndex, employeeMenuPageRequestDto.PageSize);
            var list = await _employeeMenuRespository.WherePaged(pageData, e => e.IsDelete == false,
                e => e.CreateDate, false);
			 pageData.Data = list.MapToList<EmployeeMenu, EmployeeMenuQueryDto>().ToList();
            return pageData;

        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        public async Task<EmployeeMenuQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest)
        {
            var entity = await _employeeMenuRespository.FirstOrDefaultAsync(e => e.Id == entityQueryRequest.Id);
            if (entity != null)
            {
                return entity.MapEntity<EmployeeMenu, EmployeeMenuQueryDto>();
            }
            return null;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="employeeMenuRequestDto"></param>
        /// <returns></returns>
        public async Task<bool> ModifyAsync(EmployeeMenuRequestDto employeeMenuRequestDto)
        {
            var employeeMenu = await _employeeMenuRespository.FirstOrDefaultAsync(e => e.Id == employeeMenuRequestDto.Id);
            var entity = employeeMenuRequestDto.MapToModifyEntity<EmployeeMenuRequestDto, EmployeeMenu>(employeeMenu);
            await EmployeeMenuValidatorsFilter.DoValidationAsync(_employeeMenuRespository,entity, ValidatorTypeConstants.Modify);
            return await _employeeMenuRespository.UpdateAsync(entity); 
        }

		/// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="employeeMenuRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchModifyAsync(IList<EmployeeMenuRequestDto> employeeMenuRequestDtos)
        {
            var ids = employeeMenuRequestDtos.Select(m => m.Id).ToList();
            var employeeMenus = await _employeeMenuRespository.EntitiesByExpressionAsync(e => ids.Contains(e.Id));
            var entities = employeeMenuRequestDtos.MapToModifyEntities<EmployeeMenuRequestDto,EmployeeMenu>(employeeMenus.ToList());
            await EmployeeMenuValidatorsFilter.DoValidationAsync(_employeeMenuRespository, entities, ValidatorTypeConstants.Create);
            await _employeeMenuRespository.BatchUpdateAsync(entities);
            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(EntityRequest entityRequest)
        {
            return await _employeeMenuRespository.UpdateAsync(entityRequest.Ids.ToArray(), async (e) =>
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
        /// 根据employeeId获取菜单
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
       public async Task<IList<EmployeeMenuQueryDto>> GetMenuByEmployeeIdAsync(string employeeId)
        {
            var list = await _employeeMenuRespository.EntitiesByExpressionAsync(e => e.EmployeeId == employeeId
              && e.IsDelete == false, e => new EmployeeMenuQueryDto { MenuId = e.MenuId });
            return list.ToList();
        }
        /// <summary>
        /// 根据菜单Id获取主键
        /// </summary>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        public async Task<IList<string>> GetIdsByMenuIdsAsync(IList<string> menuIds)
        {
            var list = await _employeeMenuRespository.EntitiesByExpressionAsync(o =>
            menuIds.Contains(o.MenuId) && o.IsDelete == false,
                 o => new EmployeeMenuQueryDto { Id = o.Id });
            return list.Select(o => o.Id).ToList();
        }
    }
}