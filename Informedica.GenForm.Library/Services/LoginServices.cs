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

        public Boolean Logout(ILoginUser user)
        {
            if (GenFormIdentity.GetIdentity(user.UserName) == null) throw new Exception();

            GenFormPrincipal.Logout();
            return true;
        }

        public Boolean ChangePassword(ILoginUser loginUser, String newPassword)
        {
            throw new NotImplementedException();
        }

        public ILoginUser GetLoginUser(String userName, String password)
        {
            return LoginUser.NewLoginUser(userName, password);
        }

        #endregion
    }
}

