using System;
using System.Linq;
using System.Linq.Expressions;
using Jericho.Core.Domain;
using Jericho.Core.Repositories;
using NHibernate;

namespace Jericho.Nhibernate.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }

        public virtual void Delete(T entity)
        {
            _session.Delete(entity);
        }

        public virtual T[] GetAll()
        {
            return _session.QueryOver<T>().List<T>().ToArray();
        }

        public virtual T GetById(object id)
        {
            return _session.Get<T>(id);
        }

        public virtual void SaveOrUpdate(T enity)
        {
            _session.SaveOrUpdate(enity);
        }

        public bool Exists(params Expression<Func<T, bool>>[] properties)
        {
            return _session.QueryOver<T>().CombinedWhere(properties).ToRowCountQuery().RowCount() > 0;
        }

        public bool IsUnique(int id, params Expression<Func<T, bool>>[] properties)
        {
            var rowCount = _session.QueryOver<T>().CombinedWhere(properties).ToRowCountQuery().RowCount();
            // create
            if (id == 0)
            {
                return rowCount == 0;
            }
            // update
            return rowCount <= 1;
        }
    }
}