using Jericho.Core.Domain;
using Jericho.Core.Repositories;
using NHibernate;

namespace Jericho.Nhibernate.Repositories
{
    public class TeamRepository : Repository<Team>, ITeamRepository {
        public TeamRepository(ISession session) : base(session)
        {
        }
    }
}