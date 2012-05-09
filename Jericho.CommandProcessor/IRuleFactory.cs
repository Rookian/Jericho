using System;
using System.Collections.Generic;
using Jericho.Core.Rules;

namespace Jericho.CommandProcessor
{
    public interface IRuleFactory
    {
        IEnumerable<IRule> Create(Type type);
    }
}