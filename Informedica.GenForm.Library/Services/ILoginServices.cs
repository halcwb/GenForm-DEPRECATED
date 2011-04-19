using System;
using Informedica.GenForm.Library.Security;

namespace Informedica.GenForm.Library.Services
{
    public interface ILoginServices
    {
        void Login(LoginUser user);
        bool Logout(LoginUser user);
        bool ChangePassword(LoginUser user, String newPassword);
        bool IsLoggedIn(LoginUser user);
        LoginUser GetLoginUser(String userName, String password);
    }
}
