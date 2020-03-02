using MicroService.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MicroService.Common.Extensions
{
    public static class LambdaExtensions
    {
        /// <summary>
        /// 表达式转换成KeyValList(主要用于多字段排序，并且多个字段的排序规则不一样)
        /// 如有多个字段进行排序,参数格式为
        ///  Expression<Func<Out_Scheduling, Dictionary<object, QueryOrderBy>>> orderBy = x => new Dictionary<object, QueryOrderBy>() {
        ///            { x.ID, QueryOrderBy.Desc },
        ///           { x.DestWarehouseName, QueryOrderBy.Asc }
        ///      };
        ///      返回的是new Dictionary<object, QueryOrderBy>(){{}}key为排序字段，QueryOrderBy为排序方式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static Dictionary<string, QueryOrderBy> GetExpressionToDic<T>(this Expression<Func<T, Dictionary<object, QueryOrderBy>>> expression)
        {
            return expression.GetExpressionToPair().ToList().ToDictionary(x => x.Key, x => x.Value);
        }
        /// <summary>
        /// 表达式转换成KeyValList(主要用于多字段排序，并且多个字段的排序规则不一样)
        /// 如有多个字段进行排序,参数格式为
        ///  Expression<Func<Out_Scheduling, Dictionary<object, bool>>> orderBy = x => new Dictionary<object, bool>() {
        ///            { x.ID, true },
        ///           { x.DestWarehouseName, true }
        ///      };
        ///      返回的是new Dictionary<object, bool>(){{}}key为排序字段，bool为升降序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, QueryOrderBy>> GetExpressionToPair<T>(this Expression<Func<T, Dictionary<object, QueryOrderBy>>> expression)
        {

            foreach (var exp in ((ListInitExpression)expression.Body).Initializers)
            {
                yield return new KeyValuePair<string, QueryOrderBy>(
                exp.Arguments[0] is MemberExpression ?
                (exp.Arguments[0] as MemberExpression).Member.Name.ToString()
                : ((exp.Arguments[0] as UnaryExpression).Operand as MemberExpression).Member.Name,
                 (QueryOrderBy)((exp.Arguments[1] as ConstantExpression).Value));
            }
        }
        /// <summary>
        /// 添加order数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="orderBySelector">string=排序的字段,Asc降序/Desc升序</param>
        /// <returns></returns>
        public static IQueryable<TEntity> GetIQueryableOrderBy<TEntity>(this IQueryable<TEntity> queryable, Dictionary<string, QueryOrderBy> orderBySelector)
        {
            string[] orderByKeys = orderBySelector.Select(x => x.Key).ToArray();
            IOrderedQueryable<TEntity> iOrderQuery = null;
            for (var i = 0; i < orderByKeys.Length; i++)
            {
                if (orderBySelector[orderByKeys[i]] == QueryOrderBy.Desc)
                {
                    if (i == 0)
                    {
                        iOrderQuery = queryable.OrderByDescending(orderByKeys[i].GetExpression<TEntity>());
                    }
                    else
                    {

                        iOrderQuery = iOrderQuery.ThenByDescending(orderByKeys[i].GetExpression<TEntity>());
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        iOrderQuery = queryable.OrderBy(orderByKeys[i].GetExpression<TEntity>());
                    }
                    else
                    {
                        iOrderQuery = iOrderQuery.ThenBy(orderByKeys[i].GetExpression<TEntity>());
                    }
                }
            }
            return iOrderQuery;
        }
          public static ParameterExpression GetExpressionParameter(this Type type)
        {

            return Expression.Parameter(type, "p");
        }
        /// <summary>
        /// 创建lambda表达式：p=>false
        /// object不能确认字段类型(datetime,int,string),如动态排序OrderBy(x=>x.ID)会用到此功能,返回的就是x=>x.ID
        /// Expression<Func<Out_Scheduling, object>> expression = x => x.CreateDate;任意类型的字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, object>> GetExpression<T>(this string propertyName)
        {
            return propertyName.GetExpression<T, object>(typeof(T).GetExpressionParameter());
        }
        /// <summary>
        /// 创建lambda表达式：p=>false
        /// 在已知TKey字段类型时,如动态排序OrderBy(x=>x.ID)会用到此功能,返回的就是x=>x.ID
        /// Expression<Func<Out_Scheduling, DateTime>> expression = x => x.CreateDate;指定了类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, TKey>> GetExpression<T, TKey>(this string propertyName, ParameterExpression parameter)
        {
            if (typeof(TKey).Name == "Object")
                return Expression.Lambda<Func<T, TKey>>(Expression.Convert(Expression.Property(parameter, propertyName), typeof(object)), parameter);
            return Expression.Lambda<Func<T, TKey>>(Expression.Property(parameter, propertyName), parameter);
        }
    }
}
