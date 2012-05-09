using Jericho.Core.Commands;

namespace Jericho.Core.Rules
{
    public interface IRule { }
    
    public interface IRuleFor<in TCommandMessage> : IRule where TCommandMessage : ICommandMessage
    {
        ExecutionResult Validate(TCommandMessage commandMessage);
    }
}