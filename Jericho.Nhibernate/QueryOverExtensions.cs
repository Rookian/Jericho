using Jericho.Core;
using Jericho.Core.Domain;
using NHibernate;

namespace Jericho.Nhibernate
{
    public static class QueryOverExtensions
    {
        public static PagedList<T> ToPagedList<T>(this IQueryOver<T, T> queryOver, int pageIndex, int pageSize) where T : Entity
        {
            var rowCountQuery = queryOver.ToRowCountQuery();
            var list = queryOver.Take(pageSize).Skip((pageIndex - 1) * pageSize).Future();
            var totalCount = rowCountQuery.FutureValue<int>().Value;

            return new PagedList<T>(list, pageIndex, pageSize, totalCount);
        }
    }
}