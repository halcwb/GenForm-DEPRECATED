using System;
using System.Web.Mvc;
using Ext.Direct.Mvc;
using Informedica.GenForm.Library.Services.Users;
using Informedica.GenForm.Library.Security;
using Informedica.GenForm.Mvc3.Environments;
using Informedica.GenForm.PresentationLayer.Security;
using StructureMap;

namespace Informedica.GenForm.Mvc3.Controllers
{
    public class LoginController : Controller
    {

        [Transaction, ActionName("Login")]
        public ActionResult Login(String userName, String password, String environment)
        {
            if (HttpContext != null && HttpContext.Application != null) 
                HttpContext.Application.Add("environment", environment);

            var user = GetUser(userName, password);
            LoginServices.Login(user);

            var success = LoginServices.IsLoggedIn(user);
            return this.Direct(new {success});
        }

        [Transaction]
        public ActionResult Logout(String userName)
        {
            throw new NotImplementedException();
        }

        [Transaction]
        public ActionResult ChangePassword(String userName, String currentPassword, String newPassword)
        {
            var user = GetUser(userName, currentPassword);

            LoginServices.ChangePassword(user, newPassword);
            return this.Direct(new {success = LoginServices.CheckPassword(newPassword)});
        }

        [Transaction]
        public ActionResult GetLoginPresentation(String userName, String password)
        {
            ILoginForm form = LoginForm.NewLoginForm(userName, password);
            return this.Direct(new {success = true, data = form});
        }

        private static ILoginCriteria GetUser(String userName, String password)
        {
            return LoginUser.NewLoginUser(userName, password);
        }

    }
}