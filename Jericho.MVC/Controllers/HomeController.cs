using System.Web.Mvc;
using Jericho.Core.Repositories;

namespace Jericho.MVC.Controllers
{
    public class HomeController : Controller
    {
        readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public ActionResult Index()
        {
            return View();
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
