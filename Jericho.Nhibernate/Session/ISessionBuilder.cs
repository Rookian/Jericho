using NHibernate;

namespace Jericho.Nhibernate.Session
{
    public interface ISessionBuilder
    {
        ISession GetSession();
    }
}