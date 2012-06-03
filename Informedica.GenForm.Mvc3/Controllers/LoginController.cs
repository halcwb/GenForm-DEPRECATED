using System;
using System.Web;
using System.Web.Mvc;
using Ext.Direct.Mvc;
using Informedica.DataAccess.Configurations;
using Informedica.GenForm.DataAccess;
using Informedica.GenForm.Presentation.Security;
using Informedica.GenForm.Services.Environments;
using Informedica.GenForm.Services.UserLogin;
using NHibernate;
using NHibernate.Context;
using StructureMap;

namespace Informedica.GenForm.Mvc3.Controllers
{
    public class LoginController : Controller
    {
        public const string NoEnvironmentMessage = "Environment has not been set";
        private const int ExpireTimeInHours = 1;
        public const string EnvironmentSetting = "environment";

        public ActionResult GetEnvironment()
        {
            return this.Direct(new {Environment = GetEnvironmentFromSession(), HttpContext.Session.IsNewSession });
        }

        private string GetEnvironmentFromSession()
        {
            return HttpContext.Session == null ? string.Empty : 
                   (string) (HttpContext.Session[EnvironmentSetting] ?? string.Empty);
        }

        public ActionResult SetEnvironment(String environment)
        {
            if (HttpContext.Session != null)
            {
                HttpContext.Session.Add(EnvironmentSetting, environment);
                EnvironmentServices.SetHttpSessionCache(HttpContext.Session);
                EnvironmentServices.SetEnvironment(environment);
            }

            return this.Direct(new {success = GetEnvironmentFromSession() == environment });
        }

        private void SetLoginCookie(string userName)
        {
            var expires = DateTime.Now.AddHours(ExpireTimeInHours); 
            var loginCookie = new HttpCookie("loginCookie", userName) {Expires = expires};

            if (Session != null) Session["user"] = userName;
            Response.AppendCookie(loginCookie);
        }

        public ActionResult GetLoggedInUser()
        {
            return this.Direct(HttpContext.Session != null ? new {user = HttpContext.Session["user"]} : new {user = (object) ""});
        }

        public ActionResult Logout(String userName)
        {
            throw new NotImplementedException();
        }

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
            var success = !string.IsNullOrEmpty(GetEnvironment(dto));

            if (success)
            {
                SetupDatabase();
                LoginServices.Login(dto);
                success = LoginServices.IsLoggedIn(dto.UserName);

                if (success) SetLoginCookie(dto.UserName);
            } else
            { 
                return this.Direct(new {success = false, message = NoEnvironmentMessage});
            }

            return this.Direct(new { success });
        }

        private void SetupDatabase()
        {
            var environment = (string)HttpContext.Session[EnvironmentSetting];
            var envConf = ConfigurationManager.Instance.GetConfiguration(environment);
            envConf.GetConnection();
            var conn = SessionStateManager.GetConnectionFromSessionState(HttpContext.Session);
            if (conn == null) return;

            var fact = SessionFactoryManager.GetSessionFactory(environment);
            HttpContext.Session["sessionfactory"] = fact;
            SessionStateManager.UseSessionFactoryFromApplicationOrSessionState(HttpContext.Session);
            var session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession(conn);
            SessionFactoryManager.BuildSchema(environment, session);
            CurrentSessionContext.Bind(session);
        }

        private string GetEnvironment(UserLoginDto dto)
        {
            if (!string.IsNullOrEmpty(dto.Environment)) SetEnvironment(dto.Environment);

            if (HttpContext.Session != null) return (string)HttpContext.Session[EnvironmentSetting];
            return string.Empty;
        }
    }
}