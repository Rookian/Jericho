using System;
using System.Collections.Generic;
using System.Web.Mvc;
using IDependencyResolver = System.Web.Http.Services.IDependencyResolver;

namespace Jericho.MVC
{
    public class DepdencyResolver : IDependencyResolver
    {
        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}