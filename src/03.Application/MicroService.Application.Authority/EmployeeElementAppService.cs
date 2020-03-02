
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
	/// EmployeeElement -applaction实现
	/// </summary>
	public class EmployeeElementAppService:IEmployeeElementAppService
	{
     
        private readonly IEmployeeElementRespository _employeeElementRespository;
        private readonly IMapper _mapper;
        public EmployeeElementAppService(IEmployeeElementRespository employeeElementRespository,
          IMapper mapper)
        {
            _employeeElementRespository = employeeElementRespository;
            _mapper = mapper;
        }
    
        /// <summary>
          /// 新增
          /// </summary>
          /// <param name="employeeElementRequestDto"></param>
          /// <returns></returns>
        public async Task<bool> CreateAsync(EmployeeElementRequestDto employeeElementRequestDto)
        {
         
            var employeeElement = _mapper.Map<EmployeeElementRequestDto, EmployeeElement>(employeeElementRequestDto);
            await EmployeeElementValidatorsFilter.DoValidationAsync(_employeeElementRespository,employeeElement, ValidatorTypeConstants.Create);
            return await _employeeElementRespository.InsertAsync(employeeElement);  
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="employeeElementRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchCreateAsync(IList<EmployeeElementRequestDto> employeeElementRequestDtos)
        {
            var entities = employeeElementRequestDtos.MapToList<EmployeeElementRequestDto, EmployeeElement>();
            await EmployeeElementValidatorsFilter.DoValidationAsync(_employeeElementRespository,entities, ValidatorTypeConstants.Create);
            await _employeeElementRespository.BatchInsertAsync(entities);
            return true;

        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="employeeElementRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchRemoveAsync(IList<EmployeeElementRequestDto> employeeElementRequestDtos)
        {
            var employeeIds = employeeElementRequestDtos.Select(m => m.EmployeeId).ToList();
            var elementIds = employeeElementRequestDtos.Select(m => m.ElementId).ToList();
            var employeeElements = await _employeeElementRespository.EntitiesByExpressionAsync(e => employeeIds.Contains(e.EmployeeId)
            && elementIds.Contains(e.ElementId) && e.IsDelete == false);
            var entities = employeeElementRequestDtos.MapToModifyEntities<EmployeeElementRequestDto, EmployeeElement>(employeeElements.ToList());
            await EmployeeElementValidatorsFilter.DoValidationAsync(_employeeElementRespository, entities, ValidatorTypeConstants.Create);
            await _employeeElementRespository.BatchUpdateAsync(entities);
            return true;
        }

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="employeeElementPageRequestDto"></param>
        /// <returns></returns>

        public async Task<PageData> GetPageListAsync( EmployeeElementPageRequestDto employeeElementPageRequestDto)
        {
            var pageData = new PageData(employeeElementPageRequestDto.PageIndex, employeeElementPageRequestDto.PageSize);
            var list = await _employeeElementRespository.WherePaged(pageData, e => e.IsDelete == false,
                e => e.CreateDate, false);
			 pageData.Data = list.MapToList<EmployeeElement, EmployeeElementQueryDto>().ToList();
            return pageData;

        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        public async Task<EmployeeElementQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest)
        {
            var entity = await _employeeElementRespository.FirstOrDefaultAsync(e => e.Id == entityQueryRequest.Id);
            if (entity != null)
            {
                return entity.MapEntity<EmployeeElement, EmployeeElementQueryDto>();
            }
            return null;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="employeeElementRequestDto"></param>
        /// <returns></returns>
        public async Task<bool> ModifyAsync(EmployeeElementRequestDto employeeElementRequestDto)
        {
            var employeeElement = await _employeeElementRespository.FirstOrDefaultAsync(e => e.Id == employeeElementRequestDto.Id);
            var entity = employeeElementRequestDto.MapToModifyEntity<EmployeeElementRequestDto, EmployeeElement>(employeeElement);
            await EmployeeElementValidatorsFilter.DoValidationAsync(_employeeElementRespository,entity, ValidatorTypeConstants.Modify);
            return await _employeeElementRespository.UpdateAsync(entity); 
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(EntityRequest entityRequest)
        {
            return await _employeeElementRespository.UpdateAsync(entityRequest.Ids.ToArray(), async (e) =>
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
        /// 根据remployeeId获取模块元素
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<IList<EmployeeElementQueryDto>> GetElementByEmployeeIdAsync(string employeeId)
        {
            var list = await _employeeElementRespository.EntitiesByExpressionAsync(e => e.EmployeeId == employeeId && e.IsDelete == false,
                e => new EmployeeElementQueryDto { ElementId=e.ElementId});
            return list.ToList();
        }
        /// <summary>
        /// 根据模块元素Id获取主键
        /// </summary>
        /// <param name="moduleIds"></param>
        /// <returns></returns>
        public async Task<IList<string>> GetIdsByModuleElementIdsAsync(IList<string> moduleElementIds)
        {
            var list = await _employeeElementRespository.EntitiesByExpressionAsync(o =>
            moduleElementIds.Contains(o.ElementId) && o.IsDelete == false,
                 o => new EmployeeElementQueryDto { Id = o.Id });
            return list.Select(o => o.Id).ToList();
        }
    }
}