using System.Web.Mvc;
using Ext.Direct.Mvc;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Services.Products.dto;
using Newtonsoft.Json.Linq;

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

        public ActionResult GetGenericNames()
        {
            return this.Direct(new[]
                                   {
                                       new {GenericName = "paracetamol"},
                                       new {GenericName = "dopamine"},
                                       new {GenericName = "midazolam"},
                                       new {GenericName = "morfine"},
                                       new {GenericName = "penicilline"}
                                   });
        }

        public ActionResult GetSubstanceNames()
        {
            return this.Direct(new[]
                                   {
                                       new {SubstanceName = "paracetamol"},
                                       new {SubstanceName = "dopamine"},
                                       new {SubstanceName = "midazolam"},
                                       new {SubstanceName = "morfine"},
                                       new {SubstanceName = "penicilline"}
                                   });
        }

        public ActionResult GetBrandNames()
        {
            return this.Direct(new[]
                                   {
                                       new {BrandName = "Augmentin"},
                                       new {BrandName = "Zitromax"},
                                       new {BrandName = "Dynatra"},
                                       new {BrandName = "Esmeron"},
                                       new {BrandName = "Perfalgan"}
                                   });
        }

        public ActionResult GetShapeNames()
        {
            return this.Direct(new[]
                                   {
                                       new {ShapeName = "tablet"},
                                       new {ShapeName = "infusievloeistof"},
                                       new {ShapeName = "zetpil"},
                                       new {ShapeName = "zalf"},
                                       new {ShapeName = "inhalatiepoeder"}
                                   });
        }

        public ActionResult GetPackageNames()
        {
            return this.Direct(new[]
                                   {
                                       new {PackageName = "tablet"},
                                       new {PackageName = "fles"},
                                       new {PackageName = "zetpil"},
                                       new {PackageName = "tube"},
                                       new {PackageName = "ampul"}
                                   });
        }

        public ActionResult GetUnitNames()
        {
            return this.Direct(new[]
                                   {
                                       new {UnitName = "stuk"},
                                       new {UnitName = "mL"}
                                   });
        }

        public ActionResult GetSubstanceUnitNames()
        {
            return this.Direct(new[]
                                   {
                                       new {UnitName = "microg"},
                                       new {UnitName = "mg"},
                                       new {UnitName = "mL"},
                                       new {UnitName = "gram"},
                                       new {UnitName = "mmol"}
                                   });
        }

        public ActionResult SaveProduct(ProductDto product)
        {
            return this.Direct(new { success = true, data = product});
        }

        public ActionResult GetProduct(JObject idObject)
        {
            return this.Direct(new { success = true, data = GetProduct() });
        }

        private static IProduct GetProduct()
        {
            return new Product
                       {
                           BrandName = "Dynatra",
                           GenericName = "dopamine",
                           PackageName = "ampul",
                           ProductCode = "1",
                           ProductId = 1,
                           ProductName = "dopamine Dynatra 5 mL ampul",
                           Quantity = 5,
                           ShapeName = "infusievloeistof",
                           UnitName = "mL"
                       };
        }
    }
}
