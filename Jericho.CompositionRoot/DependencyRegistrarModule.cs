using System;
using System.Linq;
using System.Web;
using Jericho.CompositionRoot.Registries;
using Jericho.Core.Commands;
using Jericho.Core.Commands.CommandMessages;
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
                        ObjectFactory.Initialize(init => typeof(MVCRegistry).Assembly.GetTypes()
                                                        .Where(type => typeof(Registry).IsAssignableFrom(type))
                                                        .Select(Activator.CreateInstance)
                                                        .Cast<Registry>().ToList()
                                                        .ForEach(init.AddRegistry));
                        _dependenciesRegistered = true;
                    }
                }
            }
        }
    }
}
