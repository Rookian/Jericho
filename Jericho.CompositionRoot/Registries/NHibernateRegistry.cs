using Jericho.Core;
using Jericho.Core.Repositories;
using Jericho.Nhibernate.Repositories;
using Jericho.Nhibernate.SessionFactory;
using Jericho.Nhibernate.UnitOfWork;
using NHibernate;
using StructureMap.Configuration.DSL;

namespace Jericho.CompositionRoot.Registries
{
    public class NHibernateRegistry : Registry
    {
        public NHibernateRegistry()
        {
            For<IUnitOfWork>().Use<UnitOfWork>();
            For<ISessionFactory>().Singleton().Use(new ConfigurationFactory().Build().BuildSessionFactory());
            
            For<ISession>().HttpContextScoped().Use(ctx =>
            {
                var currentSessionFactory = ctx.GetInstance<ISessionFactory>();
                var session = currentSessionFactory.OpenSession();
                session.FlushMode = FlushMode.Commit;
                return session;
            });

            Scan(x =>
            {
                x.WithDefaultConventions();
                x.AssemblyContainingType(typeof(TeamEmployeeRepository));
                x.ConnectImplementationsToTypesClosing(typeof(IRepository<>));
            });
        }
    }
}