using System.Web.Mvc;
using Jericho.Core;
using Jericho.Core.Commands;
using Jericho.Core.Commands.CommandMessages;
using Jericho.Core.Repositories;
using Jericho.MVC.Models;

namespace Jericho.MVC.Controllers
{
    public class HomeController : CommandController
    {
        readonly IEmployeeRepository _employeeRepository;

        public HomeController(ICommandProcessor commandProcessor, IUnitOfWork unitOfWork, IEmployeeRepository employeeRepository)
            : base(commandProcessor, unitOfWork)
        {
            _employeeRepository = employeeRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(EmployeeViewModel model)
        {
            var createOrUpdateEmployeeMessage = new CreateOrUpdateEmployeeMessage { EMail = model.EMail, FirstName = model.FirstName, Id = model.Id, Infos = model.Infos, LastName = model.LastName };
            return Command<CreateOrUpdateEmployeeMessage, EmployeeViewModel>(createOrUpdateEmployeeMessage, s => Index(), f => Contact(model));
        }

        public ActionResult About()
        {
            return View("About");
        }

        public ActionResult Contact(EmployeeViewModel message)
        {
            return View(message);
            
        }

        public JsonResult IsMailUnique(string email)
        {
            var exists = _employeeRepository.Exists(x => x.EMail == email);

            return Json(exists ? (object)"Gibt's schon!" : (object)true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsEmployeeUnique(string FirstName, string LastName)
        {
            var exists = _employeeRepository.Exists(x => x.FirstName == FirstName, x => x.LastName == LastName);
            return Json(!exists ? (object)true : (object)"Gibt's schon!", JsonRequestBehavior.AllowGet);
        }
    }
}
