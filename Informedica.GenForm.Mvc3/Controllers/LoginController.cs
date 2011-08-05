using System;
using System.Web.Mvc;
using Ext.Direct.Mvc;
using Informedica.GenForm.Library.Services.Users;
using Newtonsoft.Json.Linq;
using Informedica.GenForm.Assembler;
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
            GetLoginServices().Login(user);

            // ReSharper disable RedundantAnonymousTypePropertyName
            return this.Direct(new { success = GetLoginServices().IsLoggedIn(user) });
            // ReSharper restore RedundantAnonymousTypePropertyName
        }

        private static JsonResult CreateEmptyLoginUser()
        {
            var result = new JsonResult
                             {
                                 Data = new
                                            {
                                                success = false,
                                                username = "gebruikernaam",
                                                password = "paswoord",
                                                validationRules =
                                     new[]
                                         {
                                             new {type = "presence", field = "username"},
                                             new {type = "presence", field = "password"}
                                         }
                                            }
                             };

            return result;
        }

        [ActionName("Login")]
        public ActionResult Login(String userName, String password)
        {
            var user = GetUser(userName, password);
            GetLoginServices().Login(user);

// ReSharper disable RedundantAnonymousTypePropertyName
            return this.Direct(new {success = GetLoginServices().IsLoggedIn(user)});
// ReSharper restore RedundantAnonymousTypePropertyName
        }

        private static ILoginCriteria GetUser(String userName, String password)
        {
            return LoginUser.NewLoginUser(userName, password);
        }

        private static ILoginServices GetLoginServices()
        {
            return LoginServices.NewLoginServices();
        }

        public ActionResult Logout(String userName)
        {
            throw new NotImplementedException();
        }

        public ActionResult ChangePassword(String userName, String currentPassword, String newPassword)
        {
            var user = GetUser(userName, currentPassword);

            GetLoginServices().ChangePassword(user, newPassword);
// ReSharper disable RedundantAnonymousTypePropertyName
            return this.Direct(new {success = GetLoginServices().CheckPassword(newPassword)});
// ReSharper restore RedundantAnonymousTypePropertyName
        }

        public ActionResult GetLoginPresentation(String userName, String password)
        {
            ILoginForm form = LoginForm.NewLoginForm(userName, password);
            return this.Direct(new {success = true, data = form});
        }

    }
}