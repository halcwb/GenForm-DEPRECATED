using System;

namespace Informedica.GenForm.Library.Security
{
    public interface ILoginUser
    {
        String UserName { get; set; }
        String Password { get; set; }
    }
}
