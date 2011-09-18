using System;

namespace Informedica.GenForm.Library.Security
{
    public interface ILoginCriteria
    {
        String UserName { get; }
        String Password { get; }
    }
}
