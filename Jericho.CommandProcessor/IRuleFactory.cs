using System;
using System.Collections.Generic;

namespace Jericho.CommandProcessor
{
    public interface IRuleFactory
    {
        IEnumerable<IRule> Create(Type type);
    }
}