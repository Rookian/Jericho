using Jericho.Core;
using Jericho.Core.Domain;
using Jericho.Core.Repositories;
using Jericho.Nhibernate.Repositories;
using Jericho.Nhibernate.SessionFactory;
using Jericho.Nhibernate.UnitOfWork;
using NHibernate;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

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

            For(typeof (IRepository<>)).Use(new Factory<Employee>().Create(""));

            
            For(typeof (IFactory<>)).Use(typeof (Factory<>));

            For(typeof (IRepository<>)).Use(x =>
                                                {
                                                    var factory = ObjectFactory.GetInstance(typeof (IFactory<>));
                                                    return factory.;
                                                });

            Scan(x =>
            {
                x.WithDefaultConventions();
                x.AssemblyContainingType(typeof(TeamEmployeeRepository));
                x.ConnectImplementationsToTypesClosing(typeof(IRepository<>));
            });
        }
    }

    public interface IFactory<T> where T : Entity
    {
        IRepository<T> Create(object obj);
    }

    public class Factory<T> : IFactory<T> where T : Entity
    {
        public IRepository<T> Create(object obj)
        {
            return null;
        }
    }
}