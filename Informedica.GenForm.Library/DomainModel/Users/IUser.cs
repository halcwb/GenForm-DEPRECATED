using System;
using Informedica.GenForm.Library.Security;

namespace Informedica.GenForm.Library.DomainModel.Users
{
    public interface IUser: IGenFormIdentity
    {
        String UserName { get; set; }
        String Password { get; set; }
        String LastName { get; set; }
        String FirstName { get; set; }
        String Email { get; set; }
        String Pager { get; set; }
    }
}
