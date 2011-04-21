using System;
using System;
using Informedica.GenForm.Library.Security;

namespace Informedica.GenForm.Library.Services
{
    public class LoginServices : ILoginServices
    {
        #region Implementation of ILoginServices

        public void Login(ILoginCriteria user)
        {
            GenFormPrincipal.Login(user);
        }

        public Boolean IsLoggedIn(ILoginCriteria user)
        {
            return GenFormPrincipal.GetPrincipal().IsLoggedIn();
        }

        public void Logout(ILoginCriteria user)
        {
            if (GenFormIdentity.GetIdentity(user.UserName) == null) throw new Exception();

            GenFormPrincipal.Logout();
        }

        public void ChangePassword(ILoginCriteria loginUser, string newPassword)
        {
            Principal.ChangePassword(loginUser.Password, newPassword);
        }

        public bool CheckPassword(String password)
        {
            return Principal.CheckPassword(password);
        }

        private static IGenFormPrincipal Principal
        {
            get { return GenFormPrincipal.GetPrincipal(); }
        }

        public ILoginCriteria CreateLoginUser(String userName, String password)
        {
            return LoginUser.NewLoginUser(userName, password);
        }

        #endregion

        #region LoginServices Factory Methods

        private LoginServices() {}

        public static ILoginServices NewLoginServices()
        {
            return new LoginServices();
        }

        #endregion
    }
}

