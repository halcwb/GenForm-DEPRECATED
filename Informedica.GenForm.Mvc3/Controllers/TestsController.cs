using System.Web.Mvc;

namespace Informedica.GenForm.Mvc3.Controllers
{
    public class TestsController : Controller
    {
        //
        // GET: /Tests/

        public ActionResult Index()
        {
            ViewBag.Title = "GenForm Tests";
            return View();
        }

    }
}
