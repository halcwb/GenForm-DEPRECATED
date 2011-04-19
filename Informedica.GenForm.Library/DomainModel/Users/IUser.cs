using System;

namespace Informedica.GenForm.Library.DomainModel.Users
{
    public interface IUser
    {
        String UserName { get; }
        String Password { get; }
        String LastName { get; }
        String FirstName { get; }
        String Email { get; }
        String Pager { get; }
    }
}
