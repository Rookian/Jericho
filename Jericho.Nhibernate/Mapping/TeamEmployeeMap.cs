using FluentNHibernate.Mapping;
using Jericho.Core.Domain;

namespace Jericho.Nhibernate.Mapping
{
    public class TeamEmployeeMap : ClassMap<TeamEmployee>
    {
        public TeamEmployeeMap()
        {
            Id(x => x.Id)
                .GeneratedBy.Identity()
                .Column("TeamEmployeeId");

            References(x => x.Employee)
                .Cascade.SaveUpdate()
                .Column("EmployeeId");

            References(x => x.Team)
                .Cascade.SaveUpdate()
                .Column("TeamId");
        }
    }
}