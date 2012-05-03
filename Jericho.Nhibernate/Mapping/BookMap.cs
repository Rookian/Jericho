using FluentNHibernate.Mapping;
using Jericho.Core.Domain;

namespace Jericho.Nhibernate.Mapping
{
    public sealed class BookMap : SubclassMap<Book>
    {
        public BookMap()
        {
            // identity mapping
            DiscriminatorValue(DiscriminatorValueLoanedItemEnum.Book);

            // column mapping
            Map(p => p.Author);
            Map(p => p.Isbn);
        }
    }


}