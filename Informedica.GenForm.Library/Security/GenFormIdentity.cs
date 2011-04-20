using System;
using System.Security.Principal;
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
            return CreateIdentity(name);
        }

        private static IGenFormIdentity CreateIdentity(string name)
        {
            var user = User.GetUser(name);
            return new GenFormIdentity(user.Name);
        }

        internal static IGenFormIdentity GetIdentity(ILoginUser user)
        {
            return CreateIdentity(user.UserName);
        }
    }
}
