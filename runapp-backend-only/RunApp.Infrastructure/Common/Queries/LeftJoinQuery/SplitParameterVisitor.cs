using System.Linq.Expressions;
using System.Reflection;

namespace RunApp.Infrastructure.Common.Queries.LeftJoinQuery
{
    internal sealed class SplitParameterVisitor<T, TResult,T1, T2> : ExpressionVisitor
    {
        private readonly MemberExpression _arg1MemberExpression;
        private readonly MemberExpression _arg2MemberExpression;
        private readonly Expression<Func<T1, T2, TResult>> _expression;
        private readonly ParameterExpression _p1;
        private readonly ParameterExpression _p2;
        private readonly ParameterExpression _parameterExpression;

        public SplitParameterVisitor(Expression<Func<T1, T2, TResult>> expression)
        {
            _expression = expression;
            _p1 = expression.Parameters[0];
            _p2 = expression.Parameters[1];

            var type = typeof(T);

            const BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty;

            var properties = type.GetProperties(Flags).ToArray();

            var propName1 = properties.Where(x => x.PropertyType.IsAssignableTo(typeof(T1)))
                      .Select(x => x.Name)
                      .FirstOrDefault();

            var propName2 = typeof(T1) == typeof(T2) ?
                            properties.Where(x => x.PropertyType.IsAssignableTo(typeof(T2)))
                                      .Skip(1)
                                      .Select(x => x.Name)
                                      .FirstOrDefault() 
                           : properties.Where(x => x.PropertyType.IsAssignableTo(typeof(T2)))
                                       .Select(x => x.Name).FirstOrDefault();

            _parameterExpression = Expression.Parameter(type);

            _arg1MemberExpression = Expression.Property(_parameterExpression, propName1);
            _arg2MemberExpression = Expression.Property(_parameterExpression, propName2);
        }
        public Expression<Func<T, TResult>> Translate()
        {
            return Expression.Lambda<Func<T, TResult>>(base.Visit(_expression.Body), _parameterExpression);
        }
        protected override Expression VisitParameter(ParameterExpression node)
        {
            if(node == _p1)
            {
                return _arg1MemberExpression;
            }
            if(node == _p2)
            {
                return _arg2MemberExpression;
            }
            return base.VisitParameter(node);
        }
    }
}
