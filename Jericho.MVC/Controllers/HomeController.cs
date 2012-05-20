using System.Web.Mvc;
using Jericho.Core;
using Jericho.Core.Commands;
using Jericho.Core.Commands.CommandMessages;
using Jericho.Core.Domain;
using Jericho.Core.Repositories;
using Jericho.MVC.Models;
using System.Linq;

namespace Jericho.MVC.Controllers
{
    public class HomeController : CommandController
    {
        readonly IEmployeeRepository _employeeRepository;

        public HomeController(ICommandProcessor commandProcessor, IUnitOfWork unitOfWork, IEmployeeRepository employeeRepository) : base(commandProcessor, unitOfWork)
        {
            _employeeRepository = employeeRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View("About");
        }

        public JsonResult IsMailUnique(string email)
        {
            var exists = _employeeRepository.Exists(x => x.EMail == email);
            return Json(exists ? (object)"Gibt's schon!" : (object)true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsEmployeeUnique(string firstName, string lastName)
        {
            var exists = _employeeRepository.Exists(x => x.FirstName == firstName, x => x.LastName == lastName);
            return Json(!exists ? (object)true : (object)"Gibt's schon!", JsonRequestBehavior.AllowGet);
        }

        public ActionResult RenderEmployeeGrid()
        {
            var employee = _employeeRepository.GetAll().Select(empl => new EmployeeViewModel(empl));
            return PartialView("EmployeeGrid", employee);
        }

        public ActionResult EditEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            return PartialView(new EmployeeViewModel(employee));
        }

        [HttpPost]
        public ActionResult CreateEmployee(EmployeeViewModel model)
        {
            var createOrUpdateEmployeeMessage = new CreateOrUpdateEmployeeMessage { EMail = model.EMail, FirstName = model.FirstName, Id = model.Id, Infos = model.Infos, LastName = model.LastName };
            return Command(createOrUpdateEmployeeMessage, s => Index());
        }

        public ActionResult CreateEmployee()
        {
            return PartialView(new EmployeeViewModel(new Employee()));
        }
    }
}
