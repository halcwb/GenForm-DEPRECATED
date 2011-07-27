using System.Web.Mvc;
using Ext.Direct.Mvc;

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

    }
}
