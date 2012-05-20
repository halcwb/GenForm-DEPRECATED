using System.Web.Mvc;
using Ext.Direct.Mvc;
using Informedica.GenForm.Services.Environments;

namespace Informedica.GenForm.Mvc3.Controllers
{
    public class EnvironmentController : Controller
    {
        //
        // GET: /Database/

        public ActionResult GetEnvironments()
        {
            var list = EnvironmentServices.GetEnvironments();
            return this.Direct(new
            {
                success = true,
                data = list
            });
        }

        public ActionResult RegisterEnvironment(EnvironmentDto dto)
        {
            var success = EnvironmentServices.AddNewEnvironment(dto);
            return this.Direct(new {success, Environment = dto});
        }


    }
}
