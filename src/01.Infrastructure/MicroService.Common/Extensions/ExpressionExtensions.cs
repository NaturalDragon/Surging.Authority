using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MicroService.Common.Extensions
{
    public static class ExpressionExtensions
    {
        /// <summary>
        /// 合并表达式, 其中master为主表达式, slave表达式中的Lambda参数合并后会依从左至右的顺序在slave表达式中进行替换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="masterExpr">主表达式</param>
        /// <param name="slaveExpr">合并入主表达式的从表达式</param>
        /// <param name="mergeExpr">合并表达式算法, 一般为按位表达式BinaryExpression, 例如Expression.Or或者Expression.And等</param>
        /// <returns></returns>
        private static Expression<T> Compose<T>(this Expression<T> masterExpr, Expression<T> slaveExpr, Func<Expression, Expression, Expression> mergeExpr)
        {
            var replaceExpr = ParameterRebinder.ReplaceParameters<T>(slaveExpr, masterExpr);

            return Expression.Lambda<T>(mergeExpr(masterExpr.Body, replaceExpr), masterExpr.Parameters);
        }

        #region 基础的与或表达式合并, 无需判断前置条件

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> masterExpr, Expression<Func<T, bool>> slaveExpr)
        {
            return masterExpr.Compose(slaveExpr, Expression.And);
        }

        public static Expression<Func<T, bool>> And<T, P>(this Expression<Func<T, bool>> masterExpr, Expression<Func<T, bool>> slaveExpr, P condPara)
        {
            if (condPara.IsNull<P>())
            {
                return masterExpr;
            }

            return masterExpr.And(slaveExpr);
        }

        /// <summary>
        /// 如果传入的excute委托返回true，则执行express的and运算并返回新的express，否则返回原express
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="P"></typeparam>
        /// <param name="masterExpr"></param>
        /// <param name="slaveExpr"></param>
        /// <param name="condPara"></param>
        /// <param name="excute">express执行条件</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> AndIf<T, P>(this Expression<Func<T, bool>> masterExpr, Expression<Func<T, bool>> slaveExpr, P condPara, Func<P, bool> excute)
        {
            if (excute.Invoke(condPara))
                return masterExpr.And(slaveExpr, condPara);
            return masterExpr;
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> masterExpr, Expression<Func<T, bool>> slaveExpr)
        {
            return masterExpr.Compose(slaveExpr, Expression.Or);
        }

        public static Expression<Func<T, bool>> Or<T, P>(this Expression<Func<T, bool>> masterExpr, Expression<Func<T, bool>> slaveExpr, P condPara)
        {
            if (condPara.IsNull<P>())
            {
                return masterExpr;
            }

            return masterExpr.Or(slaveExpr);
        }

        #endregion

        #region 基础的与或表达式合并, 需判断前置条件

        public static Expression<Func<T, bool>> And<C, T>(this Expression<Func<T, bool>> masterExpr, Expression<Func<T, bool>> slaveExpr, C preCondPara, Func<C, bool> preConditionExpr)
        {
            return preConditionExpr(preCondPara) ? masterExpr.Compose(slaveExpr, Expression.And) : masterExpr;
        }

        public static Expression<Func<T, bool>> Or<C, T>(this Expression<Func<T, bool>> masterExpr, Expression<Func<T, bool>> slaveExpr, C preCondPara, Func<C, bool> preConditionExpr)
        {
            return preConditionExpr(preCondPara) ? masterExpr.Compose(slaveExpr, Expression.Or) : masterExpr;
        }

        #endregion
    }
}
