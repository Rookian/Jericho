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
            return View();
        }

        [HttpPost]
        public ActionResult Index(CreateOrUpdateEmployeeMessage message)
        {
            //var createOrUpdateEmployeeMessage = new CreateOrUpdateEmployeeMessage { EMail = "a", FirstName = "Alex", Infos = "", Id = 0, LastName = "Tank" };

            return Command(message, s => View());
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
