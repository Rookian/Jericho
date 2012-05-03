using System;

namespace Jericho.Core
{
    public class AbstractFactoryBase<T>
    {
        protected static T GetDefaultUnconfiguredState()
        {
            throw new Exception(String.Format("{0} is not configured", typeof(T).Name));
        }
    }
}
