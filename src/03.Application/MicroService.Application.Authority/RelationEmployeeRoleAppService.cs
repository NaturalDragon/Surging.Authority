
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
	/// RelationEmployeeRole -applaction实现
	/// </summary>
	public class RelationEmployeeRoleAppService:IRelationEmployeeRoleAppService
	{
     
        private readonly IRelationEmployeeRoleRespository _relationEmployeeRoleRespository;
        private readonly IMapper _mapper;
        public RelationEmployeeRoleAppService(IRelationEmployeeRoleRespository relationEmployeeRoleRespository,
          IMapper mapper)
        {
            _relationEmployeeRoleRespository = relationEmployeeRoleRespository;
            _mapper = mapper;
        }
    
        /// <summary>
          /// 新增
          /// </summary>
          /// <param name="relationEmployeeRoleRequestDto"></param>
          /// <returns></returns>
        public async Task<bool> CreateAsync(RelationEmployeeRoleRequestDto relationEmployeeRoleRequestDto)
        {
         
            var relationEmployeeRole = relationEmployeeRoleRequestDto.MapToCreateEntity<RelationEmployeeRoleRequestDto, RelationEmployeeRole>();
            await RelationEmployeeRoleValidatorsFilter.DoValidationAsync(_relationEmployeeRoleRespository,relationEmployeeRole, ValidatorTypeConstants.Create);
            return await _relationEmployeeRoleRespository.InsertAsync(relationEmployeeRole);  
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="relationEmployeeRoleRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchCreateAsync(IList<RelationEmployeeRoleRequestDto> relationEmployeeRoleRequestDtos)
        {
            var entities =relationEmployeeRoleRequestDtos.MapToCreateEntities<RelationEmployeeRoleRequestDto, RelationEmployeeRole>();
            await RelationEmployeeRoleValidatorsFilter.DoValidationAsync(_relationEmployeeRoleRespository,entities, ValidatorTypeConstants.Create);
            await _relationEmployeeRoleRespository.BatchInsertAsync(entities);
            return true;
         
        }

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="relationEmployeeRolePageRequestDto"></param>
        /// <returns></returns>

        public async Task<PageData> GetPageListAsync( RelationEmployeeRolePageRequestDto relationEmployeeRolePageRequestDto)
        {
            var pageData = new PageData(relationEmployeeRolePageRequestDto.PageIndex, relationEmployeeRolePageRequestDto.PageSize);
            var list = await _relationEmployeeRoleRespository.WherePaged(pageData, e => e.IsDelete == false,
                e => e.CreateDate, false);
			 pageData.Data = list.MapToList<RelationEmployeeRole, RelationEmployeeRoleQueryDto>().ToList();
            return pageData;

        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        public async Task<RelationEmployeeRoleQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest)
        {
            var entity = await _relationEmployeeRoleRespository.FirstOrDefaultAsync(e => e.Id == entityQueryRequest.Id);
            if (entity != null)
            {
                return entity.MapEntity<RelationEmployeeRole, RelationEmployeeRoleQueryDto>();
            }
            return null;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="relationEmployeeRoleRequestDto"></param>
        /// <returns></returns>
        public async Task<bool> ModifyAsync(RelationEmployeeRoleRequestDto relationEmployeeRoleRequestDto)
        {
            var relationEmployeeRole = await _relationEmployeeRoleRespository.FirstOrDefaultAsync(e => e.Id == relationEmployeeRoleRequestDto.Id);
            var entity = relationEmployeeRoleRequestDto.MapToModifyEntity<RelationEmployeeRoleRequestDto, RelationEmployeeRole>(relationEmployeeRole);
            await RelationEmployeeRoleValidatorsFilter.DoValidationAsync(_relationEmployeeRoleRespository,entity, ValidatorTypeConstants.Modify);
            return await _relationEmployeeRoleRespository.UpdateAsync(entity); 
        }

		/// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="relationEmployeeRoleRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchModifyAsync(IList<RelationEmployeeRoleRequestDto> relationEmployeeRoleRequestDtos)
        {
            var ids = relationEmployeeRoleRequestDtos.Select(m => m.Id).ToList();
            var relationEmployeeRoles = await _relationEmployeeRoleRespository.EntitiesByExpressionAsync(e => ids.Contains(e.Id));
            var entities = relationEmployeeRoleRequestDtos.MapToModifyEntities<RelationEmployeeRoleRequestDto,RelationEmployeeRole>(relationEmployeeRoles.ToList());
            await RelationEmployeeRoleValidatorsFilter.DoValidationAsync(_relationEmployeeRoleRespository, entities, ValidatorTypeConstants.Create);
            await _relationEmployeeRoleRespository.BatchUpdateAsync(entities);
            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(EntityRequest entityRequest)
        {
            return await _relationEmployeeRoleRespository.UpdateAsync(entityRequest.Ids.ToArray(), async (e) =>
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
        /// 根据成员Id和角色ids删除
        /// </summary>
        /// <param name="relationEmployeeRoleRemoveDto"></param>
        /// <returns></returns>
      public async  Task<bool> RemoveByEmployeeIdAndRoleIds(RelationEmployeeRoleRemoveDto relationEmployeeRoleRemoveDto)
        {
            return await _relationEmployeeRoleRespository.UpdateAsync(r=>r.EmployeeId== relationEmployeeRoleRemoveDto.EmployeeId&&
            relationEmployeeRoleRemoveDto.RoleIds.Contains(r.RoleId), async (e) =>
            {
                await Task.Run(() =>
                {
                    e.IsDelete = true;
                    e.ModifyUserId = relationEmployeeRoleRemoveDto.ModifyUserId;
                    e.ModifyUserName = relationEmployeeRoleRemoveDto.ModifyUserName;
                    e.ModifyDate = relationEmployeeRoleRemoveDto.ModifyDate;
                });
            });
        }
        /// <summary>
        /// 根据角色ids查找empIds
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public async Task<IList<string>> GetEmployeeIdsByRoleIds(IList<string> roleIds)
        {
            var list = await _relationEmployeeRoleRespository.EntitiesByExpressionAsync(r =>
          roleIds.Contains(r.RoleId) && r.IsDelete == false, r => new RelationEmployeeRoleQueryDto { EmployeeId = r.EmployeeId });
            return list.Select(r => r.EmployeeId).ToList();
        }

        /// <summary>
        /// 根据成员id获取角色Ids
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<IList<RelationEmployeeRoleQueryDto>> GetRoleIdsByEmployeeIds(IList<string> employeeIds)
        {
            var list = await _relationEmployeeRoleRespository.EntitiesByExpressionAsync(r =>
         employeeIds.Contains(r.EmployeeId) && r.IsDelete == false, r => new RelationEmployeeRoleQueryDto { RoleId = r.RoleId ,EmployeeId=r.EmployeeId});
            return list.ToList();
        }
        /// <summary>
        /// 查询该角色是否存在
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
       public async Task<bool> GetAnyByRoleIdAsync(string roleId)
        {
            return await _relationEmployeeRoleRespository.AnyAsync(r => r.RoleId == roleId && r.IsDelete == false);
        }
        /// <summary>
        /// 根据employeeIds获取主键
        /// </summary>
        /// <param name="employeeIds"></param>
        /// <returns></returns>
        public async Task<IList<string>> GetIdsByEmployeeIds(IList<string> employeeIds)
        {
            var list = await _relationEmployeeRoleRespository.EntitiesByExpressionAsync(r =>
           employeeIds.Contains(r.EmployeeId) && r.IsDelete == false, r => new RelationEmployeeRoleQueryDto
           {
               Id = r.Id
           });
            return list.Select(o => o.Id).ToList();
        }
    }
}