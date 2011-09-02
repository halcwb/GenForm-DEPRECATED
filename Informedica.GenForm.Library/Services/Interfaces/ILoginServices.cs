using System;
using Informedica.GenForm.Library.Security;

namespace Informedica.GenForm.Library.Services.Interfaces
{
    public interface ILoginServices
    {
        void Login(ILoginCriteria user);
        void Logout(ILoginCriteria user);
        void ChangePassword(ILoginCriteria user, String newPassword);
        bool CheckPassword(String password);
        bool IsLoggedIn(ILoginCriteria user);
        ILoginCriteria CreateLoginUser(String userName, String password);
    }
}
