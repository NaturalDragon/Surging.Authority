using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MicroService.Common.Extensions
{
    internal class ParameterRebinder : ExpressionVisitor
    {
        private Dictionary<ParameterExpression, ParameterExpression> parameterMap;

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> parameterMap)
        {
            this.parameterMap = parameterMap;
        }

        /// <summary>
        /// 替换表达式树Lambda传入的参数名称(合并后, 2个表达式需相同的Lambda参数名称)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="slaveExpr">被替换Lambda传入参数的表达式</param>
        /// <param name="masterExpr">用于替换Lambda传入参数的源表达式</param>
        /// <returns></returns>
        internal static Expression ReplaceParameters<T>(Expression<T> slaveExpr, Expression<T> masterExpr)
        {
            if (slaveExpr.Parameters.Count() != masterExpr.Parameters.Count())
                throw new ArgumentException("表达式\"" + slaveExpr.ToString() + "\"与表达式\"" + masterExpr.ToString() + "\"由于参数不对等, 无法进行与或操作");

            // 获取主表达式与从表达式Lambda参数从左至右依顺序的对应关系
            var parameterMap = masterExpr.Parameters.Select((paraSource, index) => new { paraSource, paraReplace = slaveExpr.Parameters[index] }).ToDictionary(p => p.paraReplace, p => p.paraSource);

            // 根据对应关系替换从表达式的Lambda参数为主表达式Lambda参数
            return new ParameterRebinder(parameterMap).Visit(slaveExpr.Body);
        }

        //
        // 摘要:
        //     访问 System.Linq.Expressions.ParameterExpression。
        //
        // 参数:
        //   node:
        //     要访问的表达式。
        //
        // 返回结果:
        //     如果修改了该表达式或任何子表达式，则为修改后的表达式；否则返回原始表达式。
        protected override Expression VisitParameter(ParameterExpression paraReplace)
        {
            ParameterExpression paraSource;

            if (this.parameterMap.TryGetValue(paraReplace, out paraSource))
            {
                paraReplace = paraSource;
            }

            return base.VisitParameter(paraReplace);
        }
    }
}
