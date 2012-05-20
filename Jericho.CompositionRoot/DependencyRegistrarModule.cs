using System.Web;
using Jericho.CompositionRoot.Registries;
using StructureMap;

namespace Jericho.CompositionRoot
{
    public class DependencyRegistrarModule : IHttpModule
    {
        private static bool _dependenciesRegistered;
        private static readonly object Lock = new object();

        public void Init(HttpApplication context)
        {
            context.BeginRequest += (sender, args) => EnsureDependenciesRegistered();
        }

        public void Dispose() { }

        private void EnsureDependenciesRegistered()
        {
            if (!_dependenciesRegistered)
            {
                lock (Lock)
                {
                    if (!_dependenciesRegistered)
                    {
                        ObjectFactory.ResetDefaults();
                        ObjectFactory.Initialize(init => init.Scan(scan =>
                        {
                            scan.AssemblyContainingType<NHibernateRegistry>();
                            scan.LookForRegistries();
                        }));

                        _dependenciesRegistered = true;
                    }
                }
            }
        }
    }
}
