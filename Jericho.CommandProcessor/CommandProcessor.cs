using System.Linq;
using Jericho.Core;
using Jericho.Core.Commands;

namespace Jericho.CommandProcessor
{
    public class CommandProcessor : ICommandProcessor
    {
        readonly ICommandInvoker _commandInvoker;
        readonly IUnitOfWork _unitOfWork;
        readonly IRulesEngine _rulesEngine;

        public CommandProcessor(ICommandInvoker commandInvoker, IUnitOfWork unitOfWork, IRulesEngine rulesEngine)
        {
            _commandInvoker = commandInvoker;
            _unitOfWork = unitOfWork;
            _rulesEngine = rulesEngine;
        }

        public ExecutionResult Process<TCommandMessage>(TCommandMessage commandMessage) where TCommandMessage : ICommandMessage
        {
            var result = new ExecutionResult();
            var ruleResults = _rulesEngine.ValidateMessage(commandMessage).ToList();
            result.Merge(ruleResults);

            if (result.Successful)
            {
                var executionResult = _commandInvoker.Process(commandMessage);
                result.Merge(executionResult);
            }

            if (!result.Successful)
            {
                _unitOfWork.RollBack();
            }

            return result;
        }
    }
}