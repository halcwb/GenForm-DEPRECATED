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
            return this.Direct(new []
                                   {
                                       new {DatabaseName = "Default Database"},
                                       new {DatabaseName = "TestDatabase Indurain"}
                                   });
        }

        public ActionResult SaveDatabaseRegistration(String databaseName, String machine, String connectionString)
        {
            SetSetting(machine, databaseName, connectionString);
            return this.Direct(new {success = true, databaseName});
        }


        public Boolean SetSetting(string computerName, string name, string value)
        {
            Settings.SettingsManager.Instance.Initialize(HttpContext.ApplicationInstance.Server.MapPath("~/"));
            Settings.SettingsManager.Instance.CreateSecureSetting(computerName, name, value);

            return true;
        }


    }
}
