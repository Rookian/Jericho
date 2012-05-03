using System.Collections.Generic;
using System.Linq;

namespace Jericho.Core.Domain
{
    public class Employee : Entity
    {
        public virtual string LastName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string EMail { get; set; }
        public virtual string Infos { get; set; }

        private readonly IList<LoanedItem> _loanedItems = new List<LoanedItem>();

        public virtual LoanedItem[] GetLoanedItems()
        {
            return _loanedItems.ToArray();
        }

        public virtual Employee AddLoanedItem(LoanedItem item)
        {
            _loanedItems.Add(item);
            return this;
        }

        public virtual Employee RemoveLoanedItem(LoanedItem item)
        {
            _loanedItems.Remove(item);
            return this;
        }
    }
}