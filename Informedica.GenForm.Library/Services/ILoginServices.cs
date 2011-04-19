using System;
using Informedica.GenForm.Library.Security;

namespace Informedica.GenForm.Library.Services
{
    public interface ILoginServices
    {
        void Login(ILoginUser user);
        bool Logout(ILoginUser user);
        bool ChangePassword(ILoginUser user, String newPassword);
        bool IsLoggedIn(ILoginUser user);
        ILoginUser GetLoginUser(String userName, String password);
    }
}
