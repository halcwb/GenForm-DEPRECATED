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
            return CreateIdentity(name);
        }

        private static IGenFormIdentity CreateIdentity(String name)
        {
            if (name == null) throw new ArgumentNullException("name");

            var user = User.GetUser(name).FirstOrDefault();
            if (user == null) throw new NullReferenceException("User could not be found");

            return new GenFormIdentity(user.Name);
        }

        internal static IGenFormIdentity GetIdentity(ILoginCriteria user)
        {
            return CreateIdentity(user.UserName);
        }
    }
}
