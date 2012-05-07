using System;
using System.Linq.Expressions;
using Jericho.Core.Domain;

namespace Jericho.Core.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        bool IsUnique(params Expression<Func<Employee, bool>>[] properties);
    }
}