using System.Web.Mvc;
using Ext.Direct.Mvc;

namespace Informedica.GenForm.Mvc2.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult GetGenericNames()
        {
            return this.Direct( new [] { new  {GenericName = "paracetamol"}, new {GenericName = "dopamine"} });
        }

        public ActionResult GetBrandNames()
        {
            return this.Direct( new [] { new  {BrandName = "Paracetamol"}, new {BrandName = "Dynatra"} });
        }
    }
}