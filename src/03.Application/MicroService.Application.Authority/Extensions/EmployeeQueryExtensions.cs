using MicroService.Entity.Authority;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MicroService.Common.Extensions;
using MicroService.IApplication.Authority.Dto;
namespace MicroService.Application.Authority.Extensions
{
    public static class EmployeeQueryExtensions
    {
        public static Expression<Func<Employee, bool>> GetOrgRoleEmployeeExpression(this EmployeePageRequestDto employeeQueryRequest)
        {
            var empty = string.Empty;

            Expression<Func<Employee, bool>> employeeExpressionAnd = m => m.Id.ToString() != empty&&m.IsDelete==false;

            if (employeeQueryRequest.IsKeySearch)
            {
                if (!string.IsNullOrEmpty(employeeQueryRequest.QueryKey))
                {
                    employeeExpressionAnd = employeeExpressionAnd.And(m =>
                        m.Name.Contains(employeeQueryRequest.QueryKey)
                        || m.Mobile.Contains(employeeQueryRequest.QueryKey)
                        || (m.Position != null && m.Position.Contains(employeeQueryRequest.QueryKey))
                        || (m.Email != null && m.Email.Contains(employeeQueryRequest.QueryKey)), employeeQueryRequest.QueryKey);

                }
            }
            else
            {
                employeeExpressionAnd = employeeExpressionAnd.And(m => employeeQueryRequest.Ids.Contains(m.Id), employeeQueryRequest.Ids);
            }
            return employeeExpressionAnd;
        }

        public static Expression<Func<Employee, bool>> GetEmployeeOriginalExpression(this EmployeePageOriginalRequestDto employeeQueryRequest)
        {
            var empty = string.Empty;

            Expression<Func<Employee, bool>> employeeExpressionAnd = m => m.Id.ToString() != empty
            &&m.IsDelete==false;

            if (!string.IsNullOrEmpty(employeeQueryRequest.QueryKey))
            {
                employeeExpressionAnd =
                    employeeExpressionAnd.And(m => m.Name.Contains(employeeQueryRequest.QueryKey)
                    || m.Mobile.Contains(employeeQueryRequest.QueryKey), employeeQueryRequest.QueryKey);
            }

          
            return employeeExpressionAnd;
        }
    }
}
