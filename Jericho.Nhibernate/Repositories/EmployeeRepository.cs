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

        public bool IsUnique(Employee employee)
        {
            // new employee
            if (employee.Id == 0)
            {
                return _session.QueryOver<Employee>()
                .Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName)
                .ToRowCountQuery()
                .RowCount() == 0;
            }
            // update employee 
            return _session.QueryOver<Employee>()
                       .Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName)
                       .ToRowCountQuery()
                       .RowCount() <= 1;
        }
    }
}