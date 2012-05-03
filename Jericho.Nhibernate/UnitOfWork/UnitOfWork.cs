using System;
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
                _session.Transaction.Dispose();
            }

            _session.BeginTransaction();
        }

        public void RollBack()
        {
            if (_session.Transaction.IsActive)
            {
                _session.Transaction.Rollback();
            }
        }

        public void Commit()
        {
            var transaction = _session.Transaction;
            if (!transaction.IsActive) throw new InvalidOperationException("Must call Begin() on the unit of work before committing");

            transaction.Commit();
        }

        public void Dispose()
        {
            _session.Dispose();
        }

        private bool ThereIsATransactionInProgress()
        {
            return _session.Transaction.IsActive || _session.Transaction.WasCommitted || _session.Transaction.WasRolledBack;
        }
    }
}