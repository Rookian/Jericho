using FluentNHibernate.Mapping;
using Jericho.Core.Domain;

namespace Jericho.Nhibernate.Mapping
{
    public sealed class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Table("tblEmployee");
            Id(p => p.Id)
                .Column("EmployeeId")
                .GeneratedBy.Identity();

            Map(p => p.EMail);
            Map(p => p.LastName);
            Map(p => p.FirstName);
            Map(p => p.Infos).Not.Nullable();
            
            HasMany(p => p.GetLoanedItems())
                .Access.CamelCaseField(Prefix.None)
                .Cascade.SaveUpdate()
                .KeyColumn("EmployeeId");
        }
    }
}