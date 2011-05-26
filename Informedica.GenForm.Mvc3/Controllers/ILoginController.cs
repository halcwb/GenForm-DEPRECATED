using System;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace Informedica.GenForm.Mvc3.Controllers
{
    public interface ILoginController
    {
        ActionResult Login(String userName, String password);
        ActionResult Login(JObject jObject);
        ActionResult Logout(String userName);
        ActionResult ChangePassword(String userName, String currentPassword, String newPassword);
        ActionResult GetLoginPresentation(String userName, String password);
    }
}
