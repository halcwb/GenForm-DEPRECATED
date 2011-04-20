using System;
using System;
using Informedica.GenForm.Library.Security;

namespace Informedica.GenForm.Library.Services
{
    public class LoginServices : ILoginServices
    {
        #region Implementation of ILoginServices

        public void Login(ILoginUser user)
        {
            GenFormPrincipal.Login(user);
        }

        public Boolean IsLoggedIn(ILoginUser user)
        {
            return GenFormPrincipal.IsLoggedIn();
        }

        public void Logout(ILoginUser user)
        {
            if (GenFormIdentity.GetIdentity(user.UserName) == null) throw new Exception();

            GenFormPrincipal.Logout();
        }

        public void ChangePassword(ILoginUser loginUser, string newPassword)
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

        public ILoginUser CreateLoginUser(String userName, String password)
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

