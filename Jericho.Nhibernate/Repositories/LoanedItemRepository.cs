using System.Collections.Generic;
using Jericho.Core.Domain;
using Jericho.Core.Repositories;
using NHibernate;

namespace Jericho.Nhibernate.Repositories
{
    public class LoanedItemRepository : Repository<LoanedItem>, ILoanedItemRepository
    {
        readonly ISession _session;

        public LoanedItemRepository(ISession session) : base(session)
        {
            _session = session;
        }

        public IList<LoanedItem> GetAllView()
        {
            return _session
                .QueryOver<LoanedItem>()
                .Fetch(x => x.LoanedBy).Eager
                .List<LoanedItem>();
        }
    }
}