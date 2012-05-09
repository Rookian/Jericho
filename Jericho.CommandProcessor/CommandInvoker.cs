using System;
using System.Linq;
using Jericho.Core.Commands;

namespace Jericho.CommandProcessor
{
    public class CommandInvoker : ICommandInvoker
    {
        private readonly ICommandHandlerFactory _commandHandlerFactory;

        public CommandInvoker(ICommandHandlerFactory commandHandlerFactory)
        {
            _commandHandlerFactory = commandHandlerFactory;
        }

        public ExecutionResult Process<TCommandMessage>(TCommandMessage commandMessage) where TCommandMessage : ICommandMessage
        {
            var executionResult = new ExecutionResult();
            var commandHandlerType = typeof(ICommandHandler<TCommandMessage>);
            var handlers = _commandHandlerFactory.Create(commandHandlerType);
            var commandHandlers = handlers.Cast<ICommandHandler<TCommandMessage>>();

            try
            {
                foreach (var commandHandler in commandHandlers)
                {
                    // Nur in dem jeweiligen Handler finden Datenänderungen statt
                    var result = commandHandler.Execute(commandMessage);
                    // TODO !!!
                    executionResult.SetExecutionResult(result);
                }
            }
            catch (Exception exception)
            {
                // Damit werden keine Änderungen in die DB geschrieben (UnitOfWork.Rollback wird später aufgerufen)
                executionResult.Errors.Add(new Error { Exception = exception, ErrorMessage = exception.Message });
            }

            return executionResult;
        }
    }
}