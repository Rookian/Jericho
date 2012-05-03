using Jericho.MVC;
using Jericho.Nhibernate.SessionFactory;
using NHibernate;
using StructureMap;
using StructureMap.Configuration.DSL;
using Jericho.Core;
using Jericho.Nhibernate.UnitOfWork;
using Jericho.Nhibernate.Repositories;
using Jericho.Core.Repositories;

namespace Jericho.CompositionRoot
{
    public class DependencyRegistry : Registry
    {
        public DependencyRegistry()
        {
            // UnitOfWork
            For<IUnitOfWork>().Use<UnitOfWork>();

            UnitOfWorkFactory.GetDefault = ObjectFactory.GetInstance<IUnitOfWork>;
            StructureMapControllerFactory.CreateDependencyCallback = ObjectFactory.GetInstance; 

            var sessionFactory = new ConfigurationFactory().Build().BuildSessionFactory();
            For<ISessionFactory>().Singleton().Use(sessionFactory);
            
            For<ISession>().HttpContextScoped().Use(ctx  =>
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
