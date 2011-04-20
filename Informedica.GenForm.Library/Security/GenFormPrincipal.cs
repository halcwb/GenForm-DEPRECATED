using System;
using System.Security.Principal;
using System.Threading;

namespace Informedica.GenForm.Library.Security
{
    internal class GenFormPrincipal: IGenFormPrincipal
    {
        private static IGenFormPrincipal _principal;

        #region Factory Methods

        private GenFormPrincipal(IIdentity identity)
        {
            Identity = identity;
        }

        internal static void Login(ILoginUser user)
        {
            SetPrincipal(GetIdentity(user));
        }

        private static IGenFormIdentity GetIdentity(ILoginUser user)
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

        private static void SetPrincipal(IGenFormIdentity identity)
        {
            if (identity.IsAuthenticated)
            {
                _principal = new GenFormPrincipal(identity);
                Thread.CurrentPrincipal = _principal;
            }
        } 

        public static IGenFormPrincipal GetPrincipal()
        {
            return _principal;
        }

        #endregion

        #region Implementation of IPrincipal

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }

        public IIdentity Identity { get; private set; }

        public void ChangePassword(string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public bool CheckPassword(string password)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
