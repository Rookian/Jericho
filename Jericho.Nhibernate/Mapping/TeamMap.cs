using FluentNHibernate.Mapping;
using Jericho.Core.Domain;

namespace Jericho.Nhibernate.Mapping
{
    public sealed class TeamMap : ClassMap<Team>
    {
        public TeamMap()
        {
            Id(p => p.Id)
                .Column("TeamId")
                .GeneratedBy.Identity();

            Map(p => p.Name);

            //HasManyToMany(p => p.GetEmployees())
            //    .Access.CamelCaseField(Prefix.Underscore)
            //    .Table("TeamEmployee")
            //    .ParentKeyColumn("TeamId")
            //    .ChildKeyColumn("EmployeeId")
            //    .LazyLoad()
            //    .AsSet()
            //    .Inverse()
            //    .Cascade.SaveUpdate();
        }
    }
}