using Jericho.Core.Domain;

namespace Jericho.Core.Repositories
{
    public interface ITeamEmployeeRepository : IRepository<TeamEmployee>
    {
        PagedList<TeamEmployee> GetPagedTeamEmployees(int pageIndex, int pageSize);
    }
}