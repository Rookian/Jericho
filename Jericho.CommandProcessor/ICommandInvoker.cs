using Jericho.Core.Commands;

namespace Jericho.CommandProcessor
{
    public interface ICommandInvoker
    {
        ExecutionResult Process<TCommandMessage>(TCommandMessage commandMessage) where TCommandMessage : ICommandMessage;
    }
}