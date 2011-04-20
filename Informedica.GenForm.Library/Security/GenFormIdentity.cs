using System;
using System.Security.Principal;

namespace Informedica.GenForm.Library.Security
{
    internal class GenFormIdentity: GenericIdentity, IGenFormIdentity
    {
        internal GenFormIdentity(String name) : base(name)
        {
        }

        internal GenFormIdentity(String name, String type) : base(name, type)
        {
        }

        internal static IGenFormIdentity GetIdentity(String name)
        {
            // ToDo: Go to repository to lookup identity by name
            throw new NotImplementedException();
        }

        internal static IIdentity GetIdentity(ILoginUser user)
        {
            // ToDo: Go to repository to loolup identity by name an password
            throw new NotImplementedException();
        }
    }
}
