
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
	/// RelationOrganizationEmployee -applaction实现
	/// </summary>
	public class RelationOrganizationEmployeeAppService:IRelationOrganizationEmployeeAppService
	{
     
        private readonly IRelationOrganizationEmployeeRespository _relationOrganizationEmployeeRespository;
        private readonly IMapper _mapper;
        public RelationOrganizationEmployeeAppService(IRelationOrganizationEmployeeRespository relationOrganizationEmployeeRespository,
          IMapper mapper)
        {
            _relationOrganizationEmployeeRespository = relationOrganizationEmployeeRespository;
            _mapper = mapper;
        }
    
        /// <summary>
          /// 新增
          /// </summary>
          /// <param name="relationOrganizationEmployeeRequestDto"></param>
          /// <returns></returns>
        public async Task<bool> CreateAsync(RelationOrganizationEmployeeRequestDto relationOrganizationEmployeeRequestDto)
        {
         
            var relationOrganizationEmployee = relationOrganizationEmployeeRequestDto.MapToCreateEntity<RelationOrganizationEmployeeRequestDto, RelationOrganizationEmployee>();
            await RelationOrganizationEmployeeValidatorsFilter.DoValidationAsync(_relationOrganizationEmployeeRespository,relationOrganizationEmployee, ValidatorTypeConstants.Create);
            return await _relationOrganizationEmployeeRespository.InsertAsync(relationOrganizationEmployee);  
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="relationOrganizationEmployeeRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchCreateAsync(IList<RelationOrganizationEmployeeRequestDto> relationOrganizationEmployeeRequestDtos)
        {
            var entities =relationOrganizationEmployeeRequestDtos.MapToCreateEntities<RelationOrganizationEmployeeRequestDto, RelationOrganizationEmployee>();
            await RelationOrganizationEmployeeValidatorsFilter.DoValidationAsync(_relationOrganizationEmployeeRespository,entities, ValidatorTypeConstants.Create);
            await _relationOrganizationEmployeeRespository.BatchInsertAsync(entities);
            return true;
         
        }

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="relationOrganizationEmployeePageRequestDto"></param>
        /// <returns></returns>

        public async Task<PageData> GetPageListAsync( RelationOrganizationEmployeePageRequestDto relationOrganizationEmployeePageRequestDto)
        {
            var pageData = new PageData(relationOrganizationEmployeePageRequestDto.PageIndex, relationOrganizationEmployeePageRequestDto.PageSize);
            var list = await _relationOrganizationEmployeeRespository.WherePaged(pageData, e => e.IsDelete == false,
                e => e.CreateDate, false);
			 pageData.Data = list.MapToList<RelationOrganizationEmployee, RelationOrganizationEmployeeQueryDto>().ToList();
            return pageData;

        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        public async Task<RelationOrganizationEmployeeQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest)
        {
            var entity = await _relationOrganizationEmployeeRespository.FirstOrDefaultAsync(e => e.Id == entityQueryRequest.Id);
            if (entity != null)
            {
                return entity.MapEntity<RelationOrganizationEmployee, RelationOrganizationEmployeeQueryDto>();
            }
            return null;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="relationOrganizationEmployeeRequestDto"></param>
        /// <returns></returns>
        public async Task<bool> ModifyAsync(RelationOrganizationEmployeeRequestDto relationOrganizationEmployeeRequestDto)
        {
            var relationOrganizationEmployee = await _relationOrganizationEmployeeRespository.FirstOrDefaultAsync(e => e.Id == relationOrganizationEmployeeRequestDto.Id);
            var entity = relationOrganizationEmployeeRequestDto.MapToModifyEntity<RelationOrganizationEmployeeRequestDto, RelationOrganizationEmployee>(relationOrganizationEmployee);
            await RelationOrganizationEmployeeValidatorsFilter.DoValidationAsync(_relationOrganizationEmployeeRespository,entity, ValidatorTypeConstants.Modify);
            return await _relationOrganizationEmployeeRespository.UpdateAsync(entity); 
        }

		/// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="relationOrganizationEmployeeRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchModifyAsync(IList<RelationOrganizationEmployeeRequestDto> relationOrganizationEmployeeRequestDtos)
        {
            var ids = relationOrganizationEmployeeRequestDtos.Select(m => m.Id).ToList();
            var relationOrganizationEmployees = await _relationOrganizationEmployeeRespository.EntitiesByExpressionAsync(e => ids.Contains(e.Id));
            var entities = relationOrganizationEmployeeRequestDtos.MapToModifyEntities<RelationOrganizationEmployeeRequestDto,RelationOrganizationEmployee>(relationOrganizationEmployees.ToList());
            await RelationOrganizationEmployeeValidatorsFilter.DoValidationAsync(_relationOrganizationEmployeeRespository, entities, ValidatorTypeConstants.Create);
            await _relationOrganizationEmployeeRespository.BatchUpdateAsync(entities);
            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(EntityRequest entityRequest)
        {
            return await _relationOrganizationEmployeeRespository.UpdateAsync(entityRequest.Ids.ToArray(), async (e) =>
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
        /// 根据用户Id和orgId删除
        /// </summary>
        /// <param name="relationOrganizationEmployeeRequestDto"></param>
        /// <returns></returns>
        public async Task<bool> RemoveByEmployeeIdAndOrgIds(RelationOrganizationEmployeeRemoveDto  relationOrganizationEmployeeRemoveDto)
        {
            return await _relationOrganizationEmployeeRespository.UpdateAsync(e=>e.EmployeeId== relationOrganizationEmployeeRemoveDto.EmployeeId
            && relationOrganizationEmployeeRemoveDto.OrganizationIds.Contains(e.OrganizationId), async (e) =>
            {
                await Task.Run(() =>
                {
                    e.IsDelete = true;
                    e.ModifyUserId = relationOrganizationEmployeeRemoveDto.ModifyUserId;
                    e.ModifyUserName = relationOrganizationEmployeeRemoveDto.ModifyUserName;
                    e.ModifyDate = relationOrganizationEmployeeRemoveDto.ModifyDate;
                });
            });
        }
        /// <summary>
        /// 根据机构ids查找empIds
        /// </summary>
        /// <param name="orgIds"></param>
        /// <returns></returns>
        public async Task<IList<string>> GetEmployeeIdsByOrgIds(IList<string> orgIds)
        {
            var list = await _relationOrganizationEmployeeRespository.EntitiesByExpressionAsync(r => 
            orgIds.Contains(r.OrganizationId) && r.IsDelete == false,r=>new RelationOrganizationEmployeeQueryDto{ EmployeeId=r.EmployeeId});
            return list.Select(r=>r.EmployeeId).ToList();
            
        }

        /// <summary>
        /// 根据成员id获取对应的机构id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<IList<RelationOrganizationEmployeeQueryDto>> GetOrgIdsByEmployeeIds(IList<string> employeeIds)
        {
            var list = await _relationOrganizationEmployeeRespository.EntitiesByExpressionAsync(r =>
           employeeIds.Contains( r.EmployeeId) && r.IsDelete == false, r => new RelationOrganizationEmployeeQueryDto { OrganizationId = r.OrganizationId,
           EmployeeId=r.EmployeeId});
            return list.ToList();
        }
        /// <summary>
        /// 查询该机构是否存在
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        public async Task<bool> GetAnyByOrganizationIdAsync(string organizationId)
        {
            return await _relationOrganizationEmployeeRespository.AnyAsync(o => o.IsDelete == false && o.OrganizationId == organizationId);
        }

        /// <summary>
        /// 根据employeeIds获取主键
        /// </summary>
        /// <param name="employeeIds"></param>
        /// <returns></returns>
        public async Task<IList<string>> GetIdsByEmployeeIds(IList<string> employeeIds)
        {
            var list = await _relationOrganizationEmployeeRespository.EntitiesByExpressionAsync(r =>
           employeeIds.Contains(r.EmployeeId) && r.IsDelete == false, r => new RelationOrganizationEmployeeQueryDto
           {
              Id=r.Id
           });
            return list.Select(o=>o.Id).ToList();
        }
    }
}