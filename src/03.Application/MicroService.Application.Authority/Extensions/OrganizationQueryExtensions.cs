using MicroService.Entity.Authority;
using MicroService.IApplication.Authority.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MicroService.Common.Extensions;
namespace MicroService.Application.Authority.Extensions
{
   public static class OrganizationQueryExtensions
    {
        public static Expression<Func<Organization, bool>> GetOrganizationParentTreeExpression(this OrganizationParentRequestDto organizationParentQueryRequest)
        {
            var empty = string.Empty;

            Expression<Func<Organization, bool>> organizationExpressionAnd = m => m.Id.ToString() != empty&&m.IsDelete==false;

            //有查询关键字
            if (!string.IsNullOrEmpty(organizationParentQueryRequest.QueryKey))
            {
                organizationExpressionAnd = organizationExpressionAnd.And(m => m.Name.Contains(organizationParentQueryRequest.QueryKey), organizationParentQueryRequest.QueryKey);
            }
            else
            {

                if (string.IsNullOrEmpty(organizationParentQueryRequest.ParentId) ||
                    organizationParentQueryRequest.ParentId == Guid.Empty.ToString())
                {
                    organizationExpressionAnd = organizationExpressionAnd.And(m => m.ParentId == Guid.Empty.ToString() ||
                    string.IsNullOrEmpty(m.ParentId), Guid.Empty.ToString());
                }
                else
                {
                    organizationExpressionAnd = organizationExpressionAnd.And(m => m.ParentId == organizationParentQueryRequest.ParentId, organizationParentQueryRequest.ParentId);
                }
            }
            //      organizationExpressionAnd = organizationExpressionAnd.And(m => m.CompanyId == organizationParentQueryRequest.CompanyId, organizationParentQueryRequest.CompanyId);



            return organizationExpressionAnd;
        }
    }
}
