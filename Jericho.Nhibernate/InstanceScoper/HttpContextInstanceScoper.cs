using System.Collections;
using System.Web;

namespace Jericho.Nhibernate.InstanceScoper
{
    public class HttpContextInstanceScoper<T> : InstanceScoperBase<T>
    {
        public bool IsEnabled()
        {
            return GetHttpContext() != null;
        }

        private static HttpContext GetHttpContext()
        {
            return HttpContext.Current;
        }

        protected override IDictionary GetDictionary()
        {
            return GetHttpContext().Items;
        }
    }
}