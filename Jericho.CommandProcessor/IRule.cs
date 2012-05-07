using Jericho.Core.Commands;

namespace Jericho.CommandProcessor
{
    public interface IRule { }
    
    public interface IRule<in TCommandMessage> : IRule where TCommandMessage : ICommandMessage
    {
        ExecutionResult Validate(TCommandMessage commandMessage);
    }
}