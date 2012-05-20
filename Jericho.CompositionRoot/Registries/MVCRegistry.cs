using System.Linq;
using Jericho.MVC;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Jericho.CompositionRoot.Registries
{
    public class MVCRegistry : Registry
    {
        public MVCRegistry()
        {
            StructureMapControllerFactory.CreateControllerInstance = ObjectFactory.GetInstance;

            StructureMapDependencyResolver.CreateService = type =>
                                                               {
                                                                   if (type.IsAbstract || type.IsInterface)
                                                                   {
                                                                       return ObjectFactory.TryGetInstance(type);
                                                                   }
                                                                   return ObjectFactory.GetInstance(type);
                                                               };



            StructureMapDependencyResolver.CreateServices = type => ObjectFactory.GetAllInstances<object>().Where(s => s.GetType() == type);
            
        }
    }
}