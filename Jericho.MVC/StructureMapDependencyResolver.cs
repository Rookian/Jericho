using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Jericho.MVC
{
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        public static Func<Type, object> CreateService = type => { throw new NotImplementedException("CreateService not configured"); };
        public static Func<Type, IEnumerable<object>> CreateServices = type => { throw new NotImplementedException("CreateServices not configured"); };

        public object GetService(Type serviceType)
        {
            return CreateService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return CreateServices(serviceType);
        }
    }
}