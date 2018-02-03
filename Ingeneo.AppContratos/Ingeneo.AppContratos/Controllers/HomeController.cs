using System.Web.Mvc;

namespace Ingeneo.AppContratos.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Denied()
        {
            return View();
        }

        public ActionResult Notification()
        {
            return View();
        }


    }
}