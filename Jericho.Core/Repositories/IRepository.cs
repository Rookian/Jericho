using System;
using System.Linq.Expressions;
using Jericho.Core.Domain;

namespace Jericho.Core.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        void Delete(T entity);
        T[] GetAll();
        T GetById(object id);
        void SaveOrUpdate(T enity);
        bool IsUnique(int id, params Expression<Func<T, bool>>[] properties);
        bool Exists(params Expression<Func<T, bool>>[] properties);
    }
}
