
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
	/// OrganizationElement -applaction实现
	/// </summary>
	public class OrganizationElementAppService:IOrganizationElementAppService
	{
     
        private readonly IOrganizationElementRespository _organizationElementRespository;
        private readonly IMapper _mapper;
        public OrganizationElementAppService(IOrganizationElementRespository organizationElementRespository,
          IMapper mapper)
        {
            _organizationElementRespository = organizationElementRespository;
            _mapper = mapper;
        }
    
        /// <summary>
          /// 新增
          /// </summary>
          /// <param name="organizationElementRequestDto"></param>
          /// <returns></returns>
        public async Task<bool> CreateAsync(OrganizationElementRequestDto organizationElementRequestDto)
        {
         
            var organizationElement = organizationElementRequestDto.MapToCreateEntity<OrganizationElementRequestDto, OrganizationElement>();
            await OrganizationElementValidatorsFilter.DoValidationAsync(_organizationElementRespository,organizationElement, ValidatorTypeConstants.Create);
            return await _organizationElementRespository.InsertAsync(organizationElement);  
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="organizationElementRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchCreateAsync(IList<OrganizationElementRequestDto> organizationElementRequestDtos)
        {
            var entities =organizationElementRequestDtos.MapToCreateEntities<OrganizationElementRequestDto, OrganizationElement>();
            await OrganizationElementValidatorsFilter.DoValidationAsync(_organizationElementRespository,entities, ValidatorTypeConstants.Create);
            await _organizationElementRespository.BatchInsertAsync(entities);
            return true;
         
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="organizationElementRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchRemoveAsync(IList<OrganizationElementRequestDto> organizationElementRequestDtos)
        {
            var orgIds = organizationElementRequestDtos.Select(m => m.OrganizationId).ToList();
            var elementIds = organizationElementRequestDtos.Select(m => m.ElementId).ToList();
            var organizationElements = await _organizationElementRespository.EntitiesByExpressionAsync(e => orgIds.Contains(e.OrganizationId)
            &&elementIds.Contains(e.ElementId)&&e.IsDelete==false);
            var entities = organizationElementRequestDtos.MapToModifyEntities<OrganizationElementRequestDto, OrganizationElement>(organizationElements.ToList());
            await OrganizationElementValidatorsFilter.DoValidationAsync(_organizationElementRespository, entities, ValidatorTypeConstants.Create);
            await _organizationElementRespository.BatchUpdateAsync(entities);
            return true;
        }

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="organizationElementPageRequestDto"></param>
        /// <returns></returns>

        public async Task<PageData> GetPageListAsync( OrganizationElementPageRequestDto organizationElementPageRequestDto)
        {
            var pageData = new PageData(organizationElementPageRequestDto.PageIndex, organizationElementPageRequestDto.PageSize);
            var list = await _organizationElementRespository.WherePaged(pageData, e => e.IsDelete == false,
                e => e.CreateDate, false);
			 pageData.Data = list.MapToList<OrganizationElement, OrganizationElementQueryDto>().ToList();
            return pageData;

        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        public async Task<OrganizationElementQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest)
        {
            var entity = await _organizationElementRespository.FirstOrDefaultAsync(e => e.Id == entityQueryRequest.Id);
            if (entity != null)
            {
                return entity.MapEntity<OrganizationElement, OrganizationElementQueryDto>();
            }
            return null;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="organizationElementRequestDto"></param>
        /// <returns></returns>
        public async Task<bool> ModifyAsync(OrganizationElementRequestDto organizationElementRequestDto)
        {
            var organizationElement = await _organizationElementRespository.FirstOrDefaultAsync(e => e.Id == organizationElementRequestDto.Id);
            var entity = organizationElementRequestDto.MapToModifyEntity<OrganizationElementRequestDto, OrganizationElement>(organizationElement);
            await OrganizationElementValidatorsFilter.DoValidationAsync(_organizationElementRespository,entity, ValidatorTypeConstants.Modify);
            return await _organizationElementRespository.UpdateAsync(entity); 
        }

		/// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="organizationElementRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchModifyAsync(IList<OrganizationElementRequestDto> organizationElementRequestDtos)
        {
            var ids = organizationElementRequestDtos.Select(m => m.Id).ToList();
            var organizationElements = await _organizationElementRespository.EntitiesByExpressionAsync(e => ids.Contains(e.Id));
            var entities = organizationElementRequestDtos.MapToModifyEntities<OrganizationElementRequestDto,OrganizationElement>(organizationElements.ToList());
            await OrganizationElementValidatorsFilter.DoValidationAsync(_organizationElementRespository, entities, ValidatorTypeConstants.Create);
            await _organizationElementRespository.BatchUpdateAsync(entities);
            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(EntityRequest entityRequest)
        {
            return await _organizationElementRespository.UpdateAsync(entityRequest.Ids.ToArray(), async (e) =>
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
        /// 根据机构id获取模块元素
        /// </summary>
        /// <param name="organizationIds"></param>
        /// <returns></returns>
        public async Task<IList<OrganizationElementQueryDto>> GetElementByOrganizationIdsAsync(IList<string> organizationIds)
        {
            var list = await _organizationElementRespository.EntitiesByExpressionAsync(o => organizationIds.Contains(o.OrganizationId) && o.IsDelete == false,
                o => new OrganizationElementQueryDto { ElementId = o.ElementId });
            return list.ToList();
        }
      
        /// <summary>
        /// 根据模块元素Id获取主键
        /// </summary>
        /// <param name="moduleIds"></param>
        /// <returns></returns>
        public async Task<IList<string>> GetIdsByModuleElementIdsAsync(IList<string> moduleElementIds)
        {
            var list = await _organizationElementRespository.EntitiesByExpressionAsync(o =>
            moduleElementIds.Contains(o.ElementId) && o.IsDelete == false,
                 o => new OrganizationElementQueryDto { Id = o.Id });
            return list.Select(o => o.Id).ToList();
        }
        /// <summary>
        /// 根据organizationIds获取主键
        /// </summary>
        /// <param name="organizationIds"></param>
        /// <returns></returns>
       public async Task<IList<string>> GetIdsByOrganizationIdsAsync(IList<string> organizationIds)
        {
            var list = await _organizationElementRespository.EntitiesByExpressionAsync(o =>
          organizationIds.Contains(o.OrganizationId) && o.IsDelete == false,
               o => new OrganizationElementQueryDto { Id = o.Id });
            return list.Select(o => o.Id).ToList();
        }
    }
}