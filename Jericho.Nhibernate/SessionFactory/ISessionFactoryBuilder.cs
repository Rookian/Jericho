using NHibernate;

namespace Jericho.Nhibernate.SessionFactory
{
    public interface ISessionFactoryBuilder
    {
        ISessionFactory GetFactory();
    }
}