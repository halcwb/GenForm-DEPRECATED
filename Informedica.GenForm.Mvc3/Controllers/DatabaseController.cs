using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Databases;
using Informedica.GenForm.Library.Services;
using System.Web.Mvc;
using Ext.Direct.Mvc;
using Informedica.GenForm.Library.Services.Databases;
using Informedica.GenForm.Library.Services.Interfaces;
using StructureMap;

namespace Informedica.GenForm.Mvc3.Controllers
{
    public class DatabaseController : Controller
    {
        //
        // GET: /Database/

        public ActionResult GetDatabases()
        {
            IEnumerable<String> names = GetDatabaseServices().GetDatabases();
            IList<object> list = new List<object>();
            foreach (var name in names)
            {
                list.Add(new {DatabaseName = name});
            }           
            return this.Direct(list);
        }

        public ActionResult SaveDatabaseRegistration(String databaseName, String machine, String connectionString)
        {
            SetSetting(machine, databaseName, connectionString);
            return this.Direct(new {success = true, databaseName});
        }


        public Boolean SetSetting(String computerName, String name, String value)
        {
            var setting = CreateSettings(computerName, name, value);
            GetDatabaseServices().MapSettingsPath(HttpContext.ApplicationInstance.Server.MapPath("~/"));
            GetDatabaseServices().RegisterDatabaseSetting(setting);

            return true;
        }

        private static IDatabaseServices GetDatabaseServices()
        {
            return ObjectFactory.GetInstance<IDatabaseServices>();
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
