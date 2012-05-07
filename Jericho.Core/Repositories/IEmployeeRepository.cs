using Jericho.Core.Domain;

namespace Jericho.Core.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        bool IsUnique(Employee employee);
    }
}