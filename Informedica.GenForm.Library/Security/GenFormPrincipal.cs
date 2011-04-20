using System;
using System.Security.Principal;
using System.Threading;

namespace Informedica.GenForm.Library.Security
{
    internal class GenFormPrincipal: IGenFormPrincipal
    {
        public GenFormPrincipal(IIdentity identity)
        {
            Identity = identity;
        }

        internal static void Login(ILoginUser user)
        {
            SetPrincipal(GetIdentity(user));
        }

        private static IIdentity GetIdentity(ILoginUser user)
        {
            return GenFormIdentity.GetIdentity(user);
        }

        internal static bool IsLoggedIn()
        {
            throw new NotImplementedException();
        }

        internal static void Logout()
        {
            throw new NotImplementedException();
        }

        private static void SetPrincipal(IIdentity identity)
        {
            if (identity.IsAuthenticated)
            {
                GenFormPrincipal principal = new GenFormPrincipal(identity);
                Thread.CurrentPrincipal = principal;
            }
        }

        #region Implementation of IPrincipal

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }

        public IIdentity Identity { get; private set; }

        #endregion
    }
}
