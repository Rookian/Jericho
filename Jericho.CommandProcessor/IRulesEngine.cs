using System.Collections.Generic;
using Jericho.Core.Commands;

namespace Jericho.CommandProcessor
{
    public interface IRulesEngine
    {
        IEnumerable<ExecutionResult> ValidateMessage<TCommandMessage>(TCommandMessage commandMessage) where TCommandMessage : ICommandMessage;
    }
}