using System;

namespace Informedica.GenForm.Library.Security
{
    public interface ILoginCriteria
    {
        String UserName { get; set; }
        String Password { get; set; }
    }
}
