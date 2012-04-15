using System.Linq;
using Jericho.Core.Domain;
using Jericho.Core.Repositories;
using Jericho.Nhibernate.Session;
using NHibernate;

namespace Jericho.Nhibernate.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly ISession _session;

        protected Repository()
        {
            _session = GetSession();
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

        public void Merge(T entity)
        {
            _session.Merge(entity);
        }

        protected ISession GetSession()
        {
            return new SessionBuilder().GetSession();
        }
    }
}