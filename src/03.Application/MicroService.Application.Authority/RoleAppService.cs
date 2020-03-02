
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
using MicroService.Common.Extensions;
using MicroService.Common.Core.Enums;

namespace MicroService.Application.Authority
 {
	/// <summary>
	/// Role -applaction实现
	/// </summary>
	public class RoleAppService:IRoleAppService
	{
     
        private readonly IRoleRespository _roleRespository;
        private readonly IMapper _mapper;
        public RoleAppService(IRoleRespository roleRespository,
          IMapper mapper)
        {
            _roleRespository = roleRespository;
            _mapper = mapper;
        }
    
        /// <summary>
          /// 新增
          /// </summary>
          /// <param name="roleRequestDto"></param>
          /// <returns></returns>
        public async Task<bool> CreateAsync(RoleRequestDto roleRequestDto)
        {
         
            var role = roleRequestDto.MapToCreateEntity<RoleRequestDto, Role>();
            await RoleValidatorsFilter.DoValidationAsync(_roleRespository,role, ValidatorTypeConstants.Create);
            return await _roleRespository.InsertAsync(role);  
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="roleRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchCreateAsync(IList<RoleRequestDto> roleRequestDtos)
        {
            var entities =roleRequestDtos.MapToCreateEntities<RoleRequestDto, Role>();
            await RoleValidatorsFilter.DoValidationAsync(_roleRespository,entities, ValidatorTypeConstants.Create);
            await _roleRespository.BatchInsertAsync(entities);
            return true;
         
        }

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="rolePageRequestDto"></param>
        /// <returns></returns>

        public async Task<PageData> GetPageListAsync( RolePageRequestDto rolePageRequestDto)
        {
            var pageData = new PageData(rolePageRequestDto.PageIndex, rolePageRequestDto.PageSize);
            var list = await _roleRespository.WherePaged(pageData, e => e.IsDelete == false,
                e => e.CreateDate, false);
			 pageData.Data = list.MapToList<Role, RoleQueryDto>().ToList();
            return pageData;

        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        public async Task<RoleQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest)
        {
            var entity = await _roleRespository.FirstOrDefaultAsync(e => e.Id == entityQueryRequest.Id);
            if (entity != null)
            {
                return entity.MapEntity<Role, RoleQueryDto>();
            }
            return null;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="roleRequestDto"></param>
        /// <returns></returns>
        public async Task<bool> ModifyAsync(RoleRequestDto roleRequestDto)
        {
            var role = await _roleRespository.FirstOrDefaultAsync(e => e.Id == roleRequestDto.Id);
            var entity = roleRequestDto.MapToModifyEntity<RoleRequestDto, Role>(role);
            await RoleValidatorsFilter.DoValidationAsync(_roleRespository,entity, ValidatorTypeConstants.Modify);
            return await _roleRespository.UpdateAsync(entity); 
        }

		/// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="roleRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchModifyAsync(IList<RoleRequestDto> roleRequestDtos)
        {
            var ids = roleRequestDtos.Select(m => m.Id).ToList();
            var roles = await _roleRespository.EntitiesByExpressionAsync(e => ids.Contains(e.Id));
            var entities = roleRequestDtos.MapToModifyEntities<RoleRequestDto,Role>(roles.ToList());
            await RoleValidatorsFilter.DoValidationAsync(_roleRespository, entities, ValidatorTypeConstants.Create);
            await _roleRespository.BatchUpdateAsync(entities);
            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(EntityRequest entityRequest)
        {
            return await _roleRespository.UpdateAsync(entityRequest.Ids.ToArray(), async (e) =>
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
        public async Task<IList<RoleTypeViewResponse>> GetRoleTypeList()
        {
            IList<RoleTypeViewResponse> roleTypeViews = new List<RoleTypeViewResponse>();
            var roleList = await _roleRespository.EntitiesByExpressionAsync(r => r.IsDelete == false,
                r=>new RoleQueryDto {  Name=r.Name,Id=r.Id,RoleType=(RoleType)r.RoleType});
            var roleTypeList = EnumExtension.GetEnumList<RoleType>();
            foreach (var item in roleTypeList)
            {

                roleTypeViews.Add(new RoleTypeViewResponse()
                {
                    Id = item.Id,
                    Name = item.Name,
                    RoleList = roleList.Where(r => r.RoleType == ((RoleType)item.Id)).ToList()
                });
            }
            return roleTypeViews;

        }
        /// <summary>
        /// 根据id获取名称
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
       public async Task<IList<RoleQueryDto>> GetRoleNameByIds(IList<string> roleIds)
        {
            var list = await _roleRespository.EntitiesByExpressionAsync(r => roleIds.Contains(r.Id),
                r => new RoleQueryDto { Id = r.Id, Name = r.Name });
            return list.ToList();
        }

    }
}