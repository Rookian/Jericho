namespace Jericho.Core.Commands
{
    public interface ICommandProcessor
    {
        ExecutionResult Process<TCommandMessage>(TCommandMessage commandMessage) where TCommandMessage : ICommandMessage;
    }
}