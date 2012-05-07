using System;
using System.Linq.Expressions;
using Jericho.Core.Domain;
using Jericho.Core.Repositories;
using NHibernate;

namespace Jericho.Nhibernate.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        readonly ISession _session;

        public EmployeeRepository(ISession session) : base(session)
        {
            _session = session;
        }

        public bool IsUnique(params Expression<Func<Employee, bool>>[] properties)
        {
            var combinedProperties = Combine(properties);
            var rowCount = _session.QueryOver<Employee>().Where(combinedProperties).ToRowCountQuery().RowCount();
            return rowCount == 0;
        }

        Expression<Func<Employee, bool>> Combine(Expression<Func<Employee, bool>>[] properties)
        {
            return employee => true;
        }
    }
}