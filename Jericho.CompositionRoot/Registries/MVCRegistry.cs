using System.Web.Mvc;
using Jericho.MVC;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Jericho.CompositionRoot.Registries
{
    public class MVCRegistry : Registry
    {
        public MVCRegistry()
        {
            StructureMapControllerFactory.CreateControllerInstance = type => ObjectFactory.GetInstance<Controller>();
        }
    }
}
