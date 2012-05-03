using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Jericho.CompositionRoot.Registries;
using StructureMap;
using StructureMap.Configuration.DSL;

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

        private static void EnsureDependenciesRegistered()
        {
            if (!_dependenciesRegistered)
            {
                lock (Lock)
                {
                    if (!_dependenciesRegistered)
                    {
                        ObjectFactory.ResetDefaults();
                        ObjectFactory.Initialize(x => GetStructureMapRegistries().ForEach(x.AddRegistry));
                        _dependenciesRegistered = true;
                    }
                }
            }
        }

        private static List<Registry> GetStructureMapRegistries()
        {
            var types = typeof(MVCRegistry).Assembly.GetTypes().Where(x => typeof(Registry).IsAssignableFrom(x)).ToList();
            return types.Select(Activator.CreateInstance).Cast<Registry>().ToList();
        }
    }
}
