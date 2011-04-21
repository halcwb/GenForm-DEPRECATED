using System;
using System.Security.Principal;

namespace Informedica.GenForm.Library.Security
{
    public interface IGenFormPrincipal: IPrincipal
    {
        new bool IsInRole(String role);
        void ChangePassword(String oldPassword, String newPassword);
        bool CheckPassword(String password);
        bool IsLoggedIn();
    }
}
