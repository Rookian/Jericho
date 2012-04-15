using Jericho.Core;
using Jericho.Core.Domain;
using Jericho.Core.Repositories;

namespace Jericho.Nhibernate.Repositories
{
    public class TeamEmployeeRepository : Repository<TeamEmployee>, ITeamEmployeeRepository
    {
        public PagedList<TeamEmployee> GetPagedTeamEmployees(int pageIndex, int pageSize)
        {
            return GetSession().QueryOver<TeamEmployee>()
                .Fetch(x => x.Employee).Eager
                .Fetch(x => x.Team).Eager
                .ToPagedList(pageIndex, pageSize);
        }
    }
}