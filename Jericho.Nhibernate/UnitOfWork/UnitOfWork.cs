using System;
using Jericho.Core;
using Jericho.Nhibernate.Session;
using NHibernate;

namespace Jericho.Nhibernate.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISessionBuilder _sessionBuilder;

        public UnitOfWork(ISessionBuilder sessionBuilder)
        {
            _sessionBuilder = sessionBuilder;
        }

        public void Begin()
        {
            if (ThereIsATransactionInProgress())
            {
                GetTransaction().Dispose();
            }

            GetSession().BeginTransaction();
        }

        public void RollBack()
        {
            if (GetTransaction().IsActive)
            {
                GetTransaction().Rollback();
            }
        }

        public void Dispose()
        {
            GetSession().Dispose();
        }

        public void Commit()
        {
            var transaction = GetTransaction();
            if (!transaction.IsActive)
                throw new InvalidOperationException("Must call Start() on the unit of work before committing");

            transaction.Commit();
        }

        private ISession GetSession()
        {
            return _sessionBuilder.GetSession();
        }

        private ITransaction GetTransaction()
        {
            return GetSession().Transaction;
        }

        private bool ThereIsATransactionInProgress()
        {
            return GetTransaction().IsActive || GetTransaction().WasCommitted || GetTransaction().WasRolledBack;
        }
    }
}