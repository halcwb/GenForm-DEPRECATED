using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Databases;
using System.Web.Mvc;
using Ext.Direct.Mvc;
using Informedica.GenForm.Library.Services.Databases;
using Informedica.GenForm.Mvc3.Environments;
using StructureMap;

namespace Informedica.GenForm.Mvc3.Controllers
{
    public class EnvironmentController : Controller
    {
        //
        // GET: /Database/

        [Transaction]
        public ActionResult GetEnvironments()
        {
            var names = new List<string> { "GenFormTest", "GenFormAcceptatie", "GenFormProductie" }; //ToDo: EnvironmentServices.GetDatabases();
            IList<object> list = new List<object>();
            foreach (var name in names)
            {
                list.Add(new {DatabaseName = name});
            }           
            return this.Direct(list);
        }

        [Transaction]
        public ActionResult SaveDatabaseRegistration(String databaseName, String connectionString)
        {
            var success = SetSetting(databaseName, connectionString);
            return this.Direct(new {success, databaseName});
        }


        private Boolean SetSetting(String name, String value)
        {
            var setting = CreateSettings(name, value);
            EnvironmentServices.MapSettingsPath(HttpContext.ApplicationInstance.Server.MapPath("~/"));
            EnvironmentServices.RegisterDatabaseSetting(setting);

            return true;
        }

        private static IEnvironment CreateSettings(String name, String value)
        {
            var setting = ObjectFactory.GetInstance<IEnvironment>();
            setting.ConnectionString = value;
            setting.Name = name;

            return setting;
        }


    }
}
