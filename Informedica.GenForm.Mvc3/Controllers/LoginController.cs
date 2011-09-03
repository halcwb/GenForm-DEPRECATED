using System;
using System.Web.Mvc;
using Ext.Direct.Mvc;
using Informedica.GenForm.Library.Services.Users;
using Newtonsoft.Json.Linq;
using Informedica.GenForm.Library.Security;
using Informedica.GenForm.PresentationLayer.Security;

namespace Informedica.GenForm.Mvc3.Controllers
{
    public class LoginController : Controller
    {

        [ActionName("Login2")]
        public ActionResult Login(JObject jObject)
        {
            if (jObject.Count == 0) return this.Direct(
                new
                    {
                        username = "gebruiker",
                        password = "paswoord",
                        validationRules = new []
                                              {
                         new {type = "presence", field = "username"},
                         new {type = "presence", field = "password"}
                    }
                }
            );
            var user = GetUser(jObject["username"].ToString(), jObject["password"].ToString());
            LoginServices.Login(user);

            return this.Direct(new { success = LoginServices.IsLoggedIn(user) });
        }

        [ActionName("Login")]
        public ActionResult Login(String userName, String password)
        {
            var user = GetUser(userName, password);
            LoginServices.Login(user);

            return this.Direct(new {success = LoginServices.IsLoggedIn(user)});
        }

        private static ILoginCriteria GetUser(String userName, String password)
        {
            return LoginUser.NewLoginUser(userName, password);
        }

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

        public ActionResult GetLoginPresentation(String userName, String password)
        {
            ILoginForm form = LoginForm.NewLoginForm(userName, password);
            return this.Direct(new {success = true, data = form});
        }

    }
}