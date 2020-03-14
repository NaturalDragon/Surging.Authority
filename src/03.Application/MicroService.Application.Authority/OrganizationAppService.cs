
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
	/// Organization -applaction实现
	/// </summary>
	public class OrganizationAppService:IOrganizationAppService
	{
     
        private readonly IOrganizationRespository _organizationRespository;
        private readonly IMapper _mapper;
        public OrganizationAppService(IOrganizationRespository organizationRespository,
          IMapper mapper)
        {
            _organizationRespository = organizationRespository;
            _mapper = mapper;
        }
    
        /// <summary>
          /// 新增
          /// </summary>
          /// <param name="organizationRequestDto"></param>
          /// <returns></returns>
        public async Task<bool> CreateAsync(OrganizationRequestDto organizationRequestDto)
        {
         
            var organization = organizationRequestDto.MapToCreateEntity<OrganizationRequestDto, Organization>();
            await OrganizationValidatorsFilter.DoValidationAsync(_organizationRespository,organization, ValidatorTypeConstants.Create);
            return await _organizationRespository.InsertAsync(organization);  
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="organizationRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchCreateAsync(IList<OrganizationRequestDto> organizationRequestDtos)
        {
            var entities =organizationRequestDtos.MapToCreateEntities<OrganizationRequestDto, Organization>();
            await OrganizationValidatorsFilter.DoValidationAsync(_organizationRespository,entities, ValidatorTypeConstants.Create);
            await _organizationRespository.BatchInsertAsync(entities);
            return true;
         
        }

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="organizationPageRequestDto"></param>
        /// <returns></returns>

        public async Task<PageData> GetPageListAsync( OrganizationPageRequestDto organizationPageRequestDto)
        {
            var pageData = new PageData(organizationPageRequestDto.PageIndex, organizationPageRequestDto.PageSize);
            var list = await _organizationRespository.WherePaged(pageData, e => e.IsDelete == false,
                e => e.CreateDate, false);
			 pageData.Data = list.MapToList<Organization, OrganizationQueryDto>().ToList();
            return pageData;

        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        public async Task<OrganizationQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest)
        {
            var entity = await _organizationRespository.FirstOrDefaultAsync(e => e.Id == entityQueryRequest.Id);
            if (entity != null)
            {
                return entity.MapEntity<Organization, OrganizationQueryDto>();
            }
            return null;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="organizationRequestDto"></param>
        /// <returns></returns>
        public async Task<bool> ModifyAsync(OrganizationRequestDto organizationRequestDto)
        {
            var organization = await _organizationRespository.FirstOrDefaultAsync(e => e.Id == organizationRequestDto.Id);
            var entity = organizationRequestDto.MapToModifyEntity<OrganizationRequestDto, Organization>(organization);
            await OrganizationValidatorsFilter.DoValidationAsync(_organizationRespository,entity, ValidatorTypeConstants.Modify);
            return await _organizationRespository.UpdateAsync(entity); 
        }

		/// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="organizationRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchModifyAsync(IList<OrganizationRequestDto> organizationRequestDtos)
        {
            var ids = organizationRequestDtos.Select(m => m.Id).ToList();
            var organizations = await _organizationRespository.EntitiesByExpressionAsync(e => ids.Contains(e.Id));
            var entities = organizationRequestDtos.MapToModifyEntities<OrganizationRequestDto,Organization>(organizations.ToList());
            await OrganizationValidatorsFilter.DoValidationAsync(_organizationRespository, entities, ValidatorTypeConstants.Create);
            await _organizationRespository.BatchUpdateAsync(entities);
            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(EntityRequest entityRequest)
        {
            return await _organizationRespository.UpdateAsync(entityRequest.Ids.ToArray(), async (e) =>
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
        /// 根据父级id获取集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IList<OrganizationQueryDto>> GetOrganizationByParentId(OrganizationParentRequestDto input)
        {
            var list = await _organizationRespository.EntitiesByExpressionAsync(input.GetOrganizationParentTreeExpression(),
                o=>new OrganizationQueryDto
                {
                    Id = o.Id,
                    Name = o.Name,
                    ParentId = o.ParentId,
                    DepartmentId = o.DepartmentId,
                });
            return list.ToList(); ;
        }

        /// <summary>
        /// 根据父级id获取集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IList<OrganizationQueryDto>> GetChildrenByParentIds(List<string> parentIds)
        {
            var list = await _organizationRespository.EntitiesByExpressionAsync(o => parentIds.Contains(o.ParentId)&&o.IsDelete==false,
                 o => new OrganizationQueryDto
                 {
                     Id = o.Id,
                     Name = o.Name,
                     ParentId = o.ParentId,
                     DepartmentId = o.DepartmentId,
                 });
            return list.ToList();
        }

        /// <summary>
        /// 根据id获取名称
        /// </summary>
        /// <param name="orgIds"></param>
        /// <returns></returns>
        public async Task<IList<OrganizationQueryDto>> GetOrganizationNameByIds(IList<string> orgIds)
        {
            var list = await _organizationRespository.EntitiesByExpressionAsync(o => orgIds.Contains(o.Id) && o.IsDelete == false,
                 o => new OrganizationQueryDto
                 {
                     Id = o.Id,
                     Name = o.Name,
                 });
            return list.ToList();
        }
        /// <summary>
        /// 查询是否有下级机构
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        public async Task<bool> GetAnyByParentIdAsync(string organizationId)
        {
            return await _organizationRespository.AnyAsync(o => o.ParentId == organizationId&&o.IsDelete==false);
        }
    }
}