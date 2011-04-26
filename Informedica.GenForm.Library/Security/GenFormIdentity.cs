using System;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using Informedica.GenForm.Library.DomainModel.Users;

namespace Informedica.GenForm.Library.Security
{
    internal class GenFormIdentity: GenericIdentity, IGenFormIdentity
    {
        private GenFormIdentity(String name) : base(name)
        {
        }

        private GenFormIdentity(String name, String type) : base(name, type)
        {
        }

        internal static IGenFormIdentity GetIdentity(String name)
        {
            return CreateIdentity(name, String.Empty);
        }

        private static IGenFormIdentity CreateIdentity(String name, String password)
        {
            if (name == null) throw new ArgumentNullException("name");

            var users = User.GetUser(name);
            if (users.Count() != 1 || users.First().Password != password) return new AnonymousIdentity();
            return new GenFormIdentity(name);
        }

        internal static IGenFormIdentity GetIdentity(ILoginCriteria criteria)
        {
            return CreateIdentity(criteria.UserName, criteria.Password);
        }
    }
}
