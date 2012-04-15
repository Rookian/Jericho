using FluentNHibernate.Mapping;
using Jericho.Core.Domain;

namespace Jericho.Nhibernate.Mapping
{
    public sealed class MagazineMap : SubclassMap<Magazine>
    {
        public MagazineMap()
        {
            // identity mapping
            DiscriminatorValue(DiscriminatorValueLoanedItemEnum.Magazine);
        }
    }
}