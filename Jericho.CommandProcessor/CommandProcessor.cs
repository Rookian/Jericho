using Jericho.Core;
using Jericho.Core.Commands;

namespace Jericho.CommandProcessor
{
    public class CommandProcessor : ICommandProcessor
    {
        readonly ICommandInvoker _commandInvoker;
        readonly IUnitOfWork _unitOfWork;

        public CommandProcessor(ICommandInvoker commandInvoker, IUnitOfWork unitOfWork)
        {
            _commandInvoker = commandInvoker;
            _unitOfWork = unitOfWork;
        }

        public ExecutionResult Process<TCommandMessage>(TCommandMessage commandMessage) where TCommandMessage : ICommandMessage
        {
            // vorher k�nnte noch gepr�ft werden ob der Command �berhaupt ausgef�hrt werden soll bevor er ausgef�hrt wird (Validierung)
            var executionResult = _commandInvoker.Process(commandMessage);
            if (!executionResult.Successful)
            {
                _unitOfWork.RollBack();
            }

            return executionResult;
        }
    }
}