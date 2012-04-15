using System.Collections.Generic;
using Jericho.Core.Domain;
using Jericho.Core.Repositories;

namespace Jericho.Nhibernate.Repositories
{
    public class LoanedItemRepository : Repository<LoanedItem>, ILoanedItemRepository
    {
        public IList<LoanedItem> GetAllView()
        {
            return GetSession()
                .QueryOver<LoanedItem>()
                .Fetch(x => x.LoanedBy).Eager
                .List<LoanedItem>();
        }
    }
}