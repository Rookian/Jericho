using System.Web.Mvc;
using Jericho.Core.Repositories;

namespace Jericho.MVC.Controllers
{
    public class HomeController : Controller
    {
        readonly ITeamEmployeeRepository _teamEmployeeRepository;
        readonly ITeamRepository _teamRepository;


        public HomeController(ITeamEmployeeRepository teamEmployeeRepository, ITeamRepository teamRepository)
        {
            _teamEmployeeRepository = teamEmployeeRepository;
            _teamRepository = teamRepository;
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
