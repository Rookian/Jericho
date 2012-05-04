using Jericho.Core;
using NHibernate;

namespace Jericho.Nhibernate.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly ISession _session;

        public UnitOfWork(ISession session)
        {
            _session = session;
        }

        public void Begin()
        {
            if (ThereIsATransactionInProgress())
            {
                GetTransaction().Dispose();
            }

            _session.BeginTransaction();
        }

        public void RollBack()
        {
            if (GetTransaction().IsActive)
            {
                GetTransaction().Rollback();
            }
        }

        public void Commit()
        {
            if (!GetTransaction().IsActive) return; // Transaction was rolled back

            GetTransaction().Commit();
        }

        public void Dispose()
        {
            _session.Dispose();
        }

        private bool ThereIsATransactionInProgress()
        {
            return GetTransaction().IsActive || GetTransaction().WasCommitted || GetTransaction().WasRolledBack;
        }

        private ITransaction GetTransaction()
        {
            return _session.Transaction;
        }
    }
}