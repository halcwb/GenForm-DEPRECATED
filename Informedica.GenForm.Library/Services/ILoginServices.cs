using System;
using Informedica.GenForm.Library.Security;

namespace Informedica.GenForm.Library.Services
{
    public interface ILoginServices
    {
        void Login(ILoginUser user);
        void Logout(ILoginUser user);
        void ChangePassword(ILoginUser user, String newPassword);
        bool CheckPassword(String password);
        bool IsLoggedIn(ILoginUser user);
        ILoginUser CreateLoginUser(String userName, String password);
    }
}
