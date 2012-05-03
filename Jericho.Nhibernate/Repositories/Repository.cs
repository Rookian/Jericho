﻿using System.Linq;
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
    }
}