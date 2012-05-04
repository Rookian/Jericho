using System;
using System.Collections.Generic;
using System.Linq;
using Jericho.Core.Commands;

namespace Jericho.CommandProcessor
{
    public class CommandInvoker : ICommandInvoker
    {
        private readonly Func<Type, IEnumerable<ICommandHandler>> _commandHandlerFactory;

        public CommandInvoker(Func<Type, IEnumerable<ICommandHandler>> commandHandlerFactory)
        {
            _commandHandlerFactory = commandHandlerFactory;
        }

        public ExecutionResult Process<TCommandMessage>(TCommandMessage commandMessage) where TCommandMessage : ICommandMessage
        {
            var executionResult = new ExecutionResult();
            var commandHandlerType = typeof(ICommandHandler<>).MakeGenericType(typeof(TCommandMessage));
            var commandHandlers = _commandHandlerFactory(commandHandlerType).Cast<ICommandHandler<TCommandMessage>>();

            try
            {
                foreach (var commandHandler in commandHandlers)
                {
                    // Nur in dem jeweiligen Handler finden Datenänderungen statt
                    var result = commandHandler.Execute(commandMessage);
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