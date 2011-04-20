using System;
using System.Security.Principal;

namespace Informedica.GenForm.Library.Security
{
    public interface IGenFormPrincipal: IPrincipal
    {
        bool IsInRole(String role);
        IIdentity Identity { get; }
        void ChangePassword(String oldPassword, String newPassword);
        bool CheckPassword(String password);
    }
}
