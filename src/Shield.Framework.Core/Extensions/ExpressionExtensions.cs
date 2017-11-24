#region Usings
using System.Linq.Expressions;
using System.Reflection;
#endregion

namespace Shield.Framework.Extensions
{
    public static class ExpressionExtensions
    {
        #region Methods
        public static MemberInfo GetMemberInfo(this Expression expression)
        {
            LambdaExpression lambdaExpression = (LambdaExpression)expression;
            return
                (!(lambdaExpression.Body is UnaryExpression)
                     ? (MemberExpression)lambdaExpression.Body
                     : (MemberExpression)((UnaryExpression)lambdaExpression.Body).Operand).Member;
        }
        #endregion
    }
}