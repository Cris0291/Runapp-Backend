using System.Linq.Expressions;

namespace RunApp.Infrastructure.Common.Queries.LeftJoinQuery
{
    public static class OuterJoin
    {
        public static IQueryable<TResult> CreateOuterJoinQuery<TOuter, TInner, TKey, TResult>(this IQueryable<TOuter> outer, 
            IQueryable<TInner> inner, Expression<Func<TOuter, TKey>> outerKey, Expression<Func<TInner, TKey>> innerkey, 
            Expression<Func<TOuter, TInner?, TResult>> resultSelector)
        {
            return SplitParameter<TOuter, TInner>.Select(
                outer
                .GroupJoin(inner,
                           outerKey,
                           innerkey,
                           (o, i) => new {o, i })
                .SelectMany(x => x.i.DefaultIfEmpty(),
                            (a, b) => new { a.o, b}),
                resultSelector
                );
        }
    }
}
