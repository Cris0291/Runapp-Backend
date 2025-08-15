using System.Linq.Expressions;

namespace RunApp.Infrastructure.Common.Queries.LeftJoinQuery
{
    public static class SplitParameter<T1, T2>
    {
        public static Expression<Func<T, TResult>> Translate<T, TResult>(Expression<Func<T1, T2, TResult>> expression)
        {
            return new SplitParameterVisitor<T,TResult,T1,T2>(expression).Translate();
        }
        public static IQueryable<TResult> Select<T, TResult>(IQueryable<T> source, Expression<Func<T1, T2, TResult>> selector)
        {
            return source.Select(Translate<T, TResult>(selector));
        }
    }
}
