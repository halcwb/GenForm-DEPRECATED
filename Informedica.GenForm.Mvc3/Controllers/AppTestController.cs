using System.Web.Mvc;

namespace Informedica.GenForm.Mvc3.Controllers
{
    public class AppTestController : Controller
    {
        //
        // GET: /AppTest/

        public ActionResult Index()
        {
            ViewBag.Title = "GenForm UseCase";
            return View();
        }

    }
}
