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
    public class DatabaseController : Controller
    {
        //
        // GET: /Database/

        [Transaction]
        public ActionResult GetDatabases()
        {
            IEnumerable<String> names = DatabaseServices.GetDatabases();
            IList<object> list = new List<object>();
            foreach (var name in names)
            {
                list.Add(new {DatabaseName = name});
            }           
            return this.Direct(list);
        }

        [Transaction]
        public ActionResult SaveDatabaseRegistration(String databaseName, String machine, String connectionString)
        {
            var success = SetSetting(machine, databaseName, connectionString);
            return this.Direct(new {success, databaseName});
        }


        private Boolean SetSetting(String computerName, String name, String value)
        {
            var setting = CreateSettings(computerName, name, value);
            DatabaseServices.MapSettingsPath(HttpContext.ApplicationInstance.Server.MapPath("~/"));
            DatabaseServices.RegisterDatabaseSetting(setting);

            return true;
        }

        private static IDatabaseSetting CreateSettings(String computerName, String name, String value)
        {
            var setting = ObjectFactory.GetInstance<IDatabaseSetting>();
            setting.ConnectionString = value;
            setting.Name = name;
            setting.Machine = computerName;

            return setting;
        }


    }
}
