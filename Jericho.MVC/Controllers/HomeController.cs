using System;
using System.Web.Mvc;
using Jericho.Core;
using Jericho.Core.Repositories;

namespace Jericho.MVC.Controllers
{
    public class HomeController : Controller
    {
        readonly ITeamEmployeeRepository _teamEmployeeRepository;
        readonly ITeamRepository _teamRepository;

        public HomeController(ITeamEmployeeRepository teamEmployeeRepository, ITeamRepository teamRepository)
        {
            using (var unitOfWork = UnitOfWorkFactory.GetDefault())
            {
                unitOfWork.Begin();
                try
                {
                    _teamEmployeeRepository = teamEmployeeRepository;
                    _teamRepository = teamRepository;
                    throw new Exception("Dies ist ein Test");
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.RollBack();
                }
            }
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
