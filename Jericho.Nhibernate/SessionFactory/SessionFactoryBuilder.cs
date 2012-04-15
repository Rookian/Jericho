using Jericho.Nhibernate.InstanceScoper;
using NHibernate;
using NHibernate.Cfg;

namespace Jericho.Nhibernate.SessionFactory
{
    public class SessionFactoryBuilder : ISessionFactoryBuilder
    {
        private const string SessionFactoryKey = "sessionFactory";

        private readonly SingletonInstanceScoper<ISessionFactory> _sessionFactorySingleton =
            new SingletonInstanceScoper<ISessionFactory>();


        public ISessionFactory GetFactory()
        {
            return _sessionFactorySingleton.GetScopedInstance(SessionFactoryKey, BuildFactory);
        }

        private static ISessionFactory BuildFactory()
        {
            var cfg = new ConfigurationFactory().Build();
            var sessionFactory = cfg.BuildSessionFactory();
            return sessionFactory;
        }
    }
}