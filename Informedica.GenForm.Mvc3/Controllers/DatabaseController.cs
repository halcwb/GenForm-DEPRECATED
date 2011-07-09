using System;
using Informedica.GenForm.Library.Services;
using Newtonsoft.Json.Linq;
using System.Web.Mvc;
using Ext.Direct.Mvc;

using System.Web.Mvc;

namespace Informedica.GenForm.Mvc3.Controllers
{
    public class DatabaseController : Controller
    {
        //
        // GET: /Database/

        public ActionResult GetDatabases()
        {
            return this.Direct(new []{ new {DatabaseName = "TestDatabase Indurain"} });
        }

    }
}
