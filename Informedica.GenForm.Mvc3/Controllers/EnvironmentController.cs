using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Databases;
using System.Web.Mvc;
using Ext.Direct.Mvc;
using Informedica.GenForm.Services.Databases;
using StructureMap;

namespace Informedica.GenForm.Mvc3.Controllers
{
    public class EnvironmentController : Controller
    {
        //
        // GET: /Database/

        public ActionResult GetEnvironments()
        {
            var names = new List<string> { "GenFormTest", "GenFormAcceptatie", "GenFormProductie" }; //ToDo: EnvironmentServices.GetDatabases();
            IList<object> list = new List<object>();
            foreach (var name in names)
            {
                list.Add(new {Name = name});
            }           
            return this.Direct(list);
        }

        public ActionResult RegisterEnvironment(EnvironmentDto dto)
        {
            var success = SetSetting(dto.Name, dto.Database);
            return this.Direct(new {success, Environment = dto});
        }


        private Boolean SetSetting(String name, String value)
        {
            var setting = CreateSettings(name, value);
            EnvironmentServices.MapSettingsPath(HttpContext.ApplicationInstance.Server.MapPath("~/"));
            
            if (!EnvironmentServices.TestDatabaseConnection(setting)) return false;

            EnvironmentServices.RegisterEnvironment(setting);
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
