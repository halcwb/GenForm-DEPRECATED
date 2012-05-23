using System;
using System.Web;
using System.Web.Mvc;
using Ext.Direct.Mvc;
using Informedica.GenForm.Mvc3.Environments;
using Informedica.GenForm.Presentation.Security;
using Informedica.GenForm.Services.Environments;
using Informedica.GenForm.Services.UserLogin;

namespace Informedica.GenForm.Mvc3.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult SetEnvironment(String environment)
        {
            if (HttpContext.Session != null) HttpContext.Session.Add("environment", environment);
            return this.Direct(new {success = true});
        }


        private void SetLoginCookie()
        {
            var expires = DateTime.Now.AddHours(1);
            var loginCookie = new HttpCookie("loginCookie", LoginServices.GetLoggedIn());
            if (Session != null) Session["user"] = LoginServices.GetLoggedIn();
            loginCookie.Expires = expires;
            Response.AppendCookie(loginCookie);
        }

        public ActionResult GetLoggedInUser()
        {
            return this.Direct(HttpContext.Session != null ? new {user = HttpContext.Session["user"]} : new {user = (object) ""});
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

        private static UserLoginDto GetUser(String userName, String password)
        {
            return new UserLoginDto
                       {
                           UserName = userName,
                           Password = password
                       };
        }

        public ActionResult Login(UserLoginDto dto)
        {
            LoginServices.Login(dto);
            var success = LoginServices.IsLoggedIn(dto.UserName);

            return this.Direct(new { success });
        }
    }
}