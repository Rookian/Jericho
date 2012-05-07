using System;
using System.Web.Mvc;
using Jericho.Core.Commands;

namespace Jericho.MVC
{
    public abstract class CommandResult : ActionResult
    {

        public override void ExecuteResult(ControllerContext context)
        {
            Execute(context);
        }

        protected abstract void Execute(ControllerContext context);
    }

    public class CommandResult<TInput, TResult> : CommandResult
        where TInput : ICommandMessage
        where TResult : class
    {
        readonly TInput _message;
        readonly Func<TResult, ActionResult> _success;
        readonly Func<TInput, ActionResult> _failure;
        readonly ICommandProcessor _commandProcessor;
        TResult _result;

        public ActionResult Success
        {
            get { return _success.Invoke(_result); }
        }

        public ActionResult Failure { get { return _failure.Invoke(_message); } }

        public CommandResult(TInput message, Func<TResult, ActionResult> success, Func<TInput, ActionResult> failure, ICommandProcessor commandProcessor)
        {
            _message = message;
            _success = success;
            _failure = failure;
            _commandProcessor = commandProcessor;
        }

        protected override void Execute(ControllerContext context)
        {
            var modelState = context.Controller.ViewData.ModelState;

            if (modelState.IsValid)
            {
                var executionResult = _commandProcessor.Process(_message);
                if (executionResult.Successful)
                {
                    _result = executionResult.Result<TResult>();
                    Success.ExecuteResult(context);
                    return;
                }
                foreach (var error in executionResult.Errors)
                {
                    foreach (var invalidProperty in error.InvalidProperties)
                    {
                        modelState.AddModelError(invalidProperty, error.ErrorMessage);
                    }
                }
            }
            Failure.ExecuteResult(context);
        }
    }
}