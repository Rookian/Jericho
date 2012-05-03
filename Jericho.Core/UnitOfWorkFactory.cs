using System;

namespace Jericho.Core
{
    public class UnitOfWorkFactory : AbstractFactoryBase<IUnitOfWork>
    {
        public static Func<IUnitOfWork> GetDefault = GetDefaultUnconfiguredState;
    }
}
