
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
	/// Employee -applaction实现
	/// </summary>
	public class EmployeeAppService:IEmployeeAppService
	{
     
        private readonly IEmployeeRespository _employeeRespository;
        private readonly IMapper _mapper;
        public EmployeeAppService(IEmployeeRespository employeeRespository,
          IMapper mapper)
        {
            _employeeRespository = employeeRespository;
            _mapper = mapper;
        }
    
        /// <summary>
          /// 新增
          /// </summary>
          /// <param name="employeeRequestDto"></param>
          /// <returns></returns>
        public async Task<bool> CreateAsync(EmployeeRequestDto employeeRequestDto)
        {
         
            var employee = employeeRequestDto.MapToCreateEntity<EmployeeRequestDto, Employee>();
            await EmployeeValidatorsFilter.DoValidationAsync(_employeeRespository,employee, ValidatorTypeConstants.Create);
            return await _employeeRespository.InsertAsync(employee);  
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="employeeRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchCreateAsync(IList<EmployeeRequestDto> employeeRequestDtos)
        {
            var entities =employeeRequestDtos.MapToCreateEntities<EmployeeRequestDto, Employee>();
            await EmployeeValidatorsFilter.DoValidationAsync(_employeeRespository,entities, ValidatorTypeConstants.Create);
            await _employeeRespository.BatchInsertAsync(entities);
            return true;
         
        }

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="employeePageRequestDto"></param>
        /// <returns></returns>

        public async Task<PageData> GetPageListAsync( EmployeePageRequestDto employeePageRequestDto)
        {
            var pageData = new PageData(employeePageRequestDto.PageIndex, employeePageRequestDto.PageSize);
            var list = await _employeeRespository.WherePaged(pageData, employeePageRequestDto.GetOrgRoleEmployeeExpression(),
                e => e.CreateDate, false);
			 pageData.Data = list.MapToList<Employee, EmployeeQueryDto>().ToList();
            return pageData;

        }
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="employeePageRequestDto"></param>
        /// <returns></returns>
        public async Task<PageData> GetListPagedOriginalAsync(EmployeePageOriginalRequestDto employeePageOriginalRequestDto)
        {
            var pageData = new PageData(employeePageOriginalRequestDto.PageIndex, employeePageOriginalRequestDto.PageSize);
            var list = await _employeeRespository.WherePaged(pageData, employeePageOriginalRequestDto.GetEmployeeOriginalExpression(),
                e => e.CreateDate, false);
            pageData.Data = list.MapToList<Employee, EmployeeQueryDto>().ToList();
            return pageData;
        }
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        public async Task<EmployeeQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest)
        {
            var entity = await _employeeRespository.FirstOrDefaultAsync(e => e.Id == entityQueryRequest.Id);
            if (entity != null)
            {
                return entity.MapEntity<Employee, EmployeeQueryDto>();
            }
            return null;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="employeeRequestDto"></param>
        /// <returns></returns>
        public async Task<bool> ModifyAsync(EmployeeRequestDto employeeRequestDto)
        {
            var employee = await _employeeRespository.FirstOrDefaultAsync(e => e.Id == employeeRequestDto.Id);
            employeeRequestDto.Password = employee.Password;
            employeeRequestDto.Salt = employee.Salt;
            var entity = employeeRequestDto.MapToModifyEntity<EmployeeRequestDto, Employee>(employee);
            await EmployeeValidatorsFilter.DoValidationAsync(_employeeRespository,entity, ValidatorTypeConstants.Modify);
            return await _employeeRespository.UpdateAsync(entity); 
        }

		/// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="employeeRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchModifyAsync(IList<EmployeeRequestDto> employeeRequestDtos)
        {
            var ids = employeeRequestDtos.Select(m => m.Id).ToList();
            var employees = await _employeeRespository.EntitiesByExpressionAsync(e => ids.Contains(e.Id));
            var entities = employeeRequestDtos.MapToModifyEntities<EmployeeRequestDto,Employee>(employees.ToList());
            await EmployeeValidatorsFilter.DoValidationAsync(_employeeRespository, entities, ValidatorTypeConstants.Create);
            await _employeeRespository.BatchUpdateAsync(entities);
            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(EntityRequest entityRequest)
        {
            return await _employeeRespository.UpdateAsync(entityRequest.Ids.ToArray(), async (e) =>
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
        /// 用户登录
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<EmployeeQueryDto> EmployeeLogin(string userId)
        {
            return await _employeeRespository.SingleAsync(e => e.UserId == userId && e.IsDelete == false,
                      e => new EmployeeQueryDto { Id=e.Id,Name = e.Name, Password = e.Password, UserId = e.UserId,Salt=e.Salt });
        }
    }
}