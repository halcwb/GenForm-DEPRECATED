using System;
using System.Security.Principal;
using Informedica.GenForm.Library.Services.Users;

namespace Informedica.GenForm.Library.Security
{
    internal class GenFormIdentity: GenericIdentity, IGenFormIdentity
    {
        private GenFormIdentity(String name) : base(name)
        {
        }

        internal static IGenFormIdentity GetIdentity(String name)
        {
            return CreateIdentity(name, String.Empty);
        }

        private static IGenFormIdentity CreateIdentity(String name, String password)
        {
            if (name == null) throw new ArgumentNullException("name");

            var user = UserServices.GetUserByName(name);
            if (user == null || String.IsNullOrWhiteSpace(password) || user.Password != password) return new AnonymousIdentity();
            return new GenFormIdentity(name);
        }

        internal static IGenFormIdentity GetIdentity(ILoginCriteria criteria)
        {
            return CreateIdentity(criteria.UserName, criteria.Password);
        }
    }
}
