using System.Web.Mvc;

namespace Informedica.GenForm.Mvc3.Controllers
{
    public class DebugController : Controller
    {
        //
        // GET: /Debug/

        public ActionResult Index()
        {
            ViewBag.Title = "GenForm Debug";
            return View();
        }

    }
}
