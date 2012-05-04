using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Jericho.MVC
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        public static Func<Type, object> CreateControllerInstance = type =>
        {
            throw new InvalidOperationException("The dependency callback for the StructureMapControllerFactory is not configured!");
        };

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return base.GetControllerInstance(requestContext, controllerType);
            }
            return CreateControllerInstance(controllerType) as Controller;
        }
    }
}