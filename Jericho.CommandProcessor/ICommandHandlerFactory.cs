using System;
using System.Collections.Generic;
using Jericho.Core.Commands;

namespace Jericho.CommandProcessor
{
    public interface ICommandHandlerFactory
    {
        IEnumerable<ICommandHandler> Create(Type type);
    }
}