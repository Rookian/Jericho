using System.Collections.Generic;
using Jericho.Core.Domain;

namespace Jericho.Core.Repositories
{
    public interface ILoanedItemRepository : IRepository<LoanedItem>
    {
        IList<LoanedItem> GetAllView();
    }
}
