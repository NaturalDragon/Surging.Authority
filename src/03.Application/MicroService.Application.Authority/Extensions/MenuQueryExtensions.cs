using MicroService.Entity.Authority;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MicroService.Common.Extensions;
using MicroService.IApplication.Authority.Dto;

namespace MicroService.Application.Authority.Extensions
{
    public static class MenuQueryExtensions
    {
        public static Expression<Func<Menu, bool>> GetMenuTreeExpression(this MenuTreeQueryRequest menuQueryRequest)
        {
            var empty = string.Empty;

            Expression<Func<Menu, bool>> menuExpressionAnd = m => m.Id.ToString() != empty&&m.IsDelete==false;

            if (!string.IsNullOrEmpty(menuQueryRequest.MenuName))
            {
                menuExpressionAnd = menuExpressionAnd.And<Menu, string>(m => m.Name.Contains(menuQueryRequest.MenuName), menuQueryRequest.MenuName);
            }
            if (!string.IsNullOrEmpty(menuQueryRequest.ModuleId))
            {
                menuExpressionAnd = menuExpressionAnd.And<Menu, string>(m => !m.ModuleId.Equals(menuQueryRequest.ModuleId), menuQueryRequest.ModuleId);
            }
            if (!string.IsNullOrEmpty(menuQueryRequest.MenuId))
            {
                menuExpressionAnd = menuExpressionAnd.And<Menu, string>(m => !m.Id.Equals(menuQueryRequest.MenuId), menuQueryRequest.MenuId);
            }

            return menuExpressionAnd;
        }
    }
}
