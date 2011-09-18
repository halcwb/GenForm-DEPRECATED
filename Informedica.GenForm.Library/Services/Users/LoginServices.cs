using System;
using Informedica.GenForm.Library.Security;

namespace Informedica.GenForm.Library.Services.Users
{
    public static class LoginServices
    {

        public static void Login(ILoginCriteria criteria)
        {
            GenFormPrincipal.Login(criteria);
        }

        public static bool IsLoggedIn(ILoginCriteria criteria)
        {
            return GenFormPrincipal.GetPrincipal().IsLoggedIn();
        }

        public static void Logout(ILoginCriteria criteria)
        {
            if (GenFormIdentity.GetIdentity(criteria.UserName) == null) throw new Exception();

            GenFormPrincipal.Logout();
        }

        public static void ChangePassword(ILoginCriteria criteria, string newPassword)
        {
            Principal.ChangePassword(criteria.Password, newPassword);
        }

        public static bool CheckPassword(string password)
        {
            return Principal.CheckPassword(password);
        }

        public static ILoginCriteria CreateLoginUse(String userName, String password)
        {
            return LoginUser.NewLoginUser(userName, password);
        }

        private static IGenFormPrincipal Principal
        {
            get { return GenFormPrincipal.GetPrincipal(); }
        }


        public static string GetLoggedIn()
        {
            return GenFormPrincipal.GetPrincipal().Identity.Name;
        }
    }
}

