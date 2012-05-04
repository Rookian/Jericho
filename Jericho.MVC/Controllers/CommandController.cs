using System;
using System.Web.Mvc;
using Jericho.Core;
using Jericho.Core.Commands;

namespace Jericho.MVC.Controllers
{
    public abstract class CommandController : Controller
    {
        readonly ICommandProcessor _commandProcessor;
        readonly IUnitOfWork _unitOfWork;

        public CommandController(ICommandProcessor commandProcessor, IUnitOfWork unitOfWork)
        {
            _commandProcessor = commandProcessor;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Mit Fehler
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="message"></param>
        /// <param name="success"></param>
        /// <param name="failure"></param>
        /// <returns></returns>
        public CommandResult Command<TMessage, TResult>(TMessage message, Func<TResult, ActionResult> success, Func<TMessage, ActionResult> failure)
            where TMessage : ICommandMessage
            where TResult : class
        {
            return new CommandResult<TMessage, TResult>(message, success, failure, _commandProcessor);
        }

        /// <summary>
        /// Ohne Fehler
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public CommandResult Command<TMessage>(TMessage message, Func<TMessage, ActionResult> result) where TMessage : class, ICommandMessage
        {
            return new CommandResult<TMessage, TMessage>(message, result, result, _commandProcessor);
        }

        /// <summary>
        /// Begin WebRequest
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _unitOfWork.Begin();
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// End WebRequest
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);

            try
            {
                _unitOfWork.Commit();
            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }
    }
}