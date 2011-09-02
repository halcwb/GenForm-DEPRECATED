using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Users;

namespace Informedica.GenForm.Library.DomainModel.Equality
{
    public class UserComparer : NameComparer, IEqualityComparer<User>
    {
        public bool Equals(User x, User y)
        {
            if (ReferenceEquals(x, y)) return true;
            return EqualName(x.Name, y.Name);
        }

        public int GetHashCode(User obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}