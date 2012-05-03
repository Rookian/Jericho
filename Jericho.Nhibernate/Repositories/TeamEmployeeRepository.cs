using Jericho.Core;
using Jericho.Core.Domain;
using Jericho.Core.Repositories;
using NHibernate;

namespace Jericho.Nhibernate.Repositories
{
    public class TeamEmployeeRepository : Repository<TeamEmployee>, ITeamEmployeeRepository
    {
        readonly ISession _session;

        public TeamEmployeeRepository(ISession session) : base(session)
        {
            _session = session;
        }

        public PagedList<TeamEmployee> GetPagedTeamEmployees(int pageIndex, int pageSize)
        {
            return _session.QueryOver<TeamEmployee>()
                .Fetch(x => x.Employee).Eager
                .Fetch(x => x.Team).Eager
                .ToPagedList(pageIndex, pageSize);
        }
    }
}