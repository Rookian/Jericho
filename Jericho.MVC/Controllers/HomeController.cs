using System.Web.Mvc;
using Jericho.Core;
using Jericho.Core.Commands;
using Jericho.Core.Commands.CommandMessages;

namespace Jericho.MVC.Controllers
{
    public class HomeController : CommandController
    {
        public HomeController(ICommandProcessor commandProcessor, IUnitOfWork unitOfWork) : base(commandProcessor, unitOfWork)
        {
        }

        public ActionResult Index()
        {
            var createOrUpdateEmployeeMessage = new CreateOrUpdateEmployeeMessage { EMail = "a", FirstName = "f", Infos = "", Id = 0, LastName = "l" };

            return Command(createOrUpdateEmployeeMessage, s => View());
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
