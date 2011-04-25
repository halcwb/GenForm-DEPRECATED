using System;
using System.Security.Principal;
using System.Threading;

namespace Informedica.GenForm.Library.Security
{
    internal class GenFormPrincipal: GenericPrincipal, IGenFormPrincipal
    {
        private static IGenFormPrincipal _principal;

        #region Factory Methods

        private GenFormPrincipal(IGenFormIdentity identity): base(identity, new String[] {})
        {
            Identity = identity;
        }

        internal static void Login(ILoginCriteria user)
        {
            SetPrincipal(GetIdentity(user));
        }

        private static IGenFormIdentity GetIdentity(ILoginCriteria user)
        {
            return GenFormIdentity.GetIdentity(user);
        }

        public bool IsLoggedIn()
        {
            return Thread.CurrentPrincipal == this && Identity.IsAuthenticated;
        }

        internal static void Logout()
        {
            throw new NotImplementedException();
        }

        private static void SetPrincipal(IGenFormIdentity identity)
        {
            _principal = new GenFormPrincipal(identity);
            Thread.CurrentPrincipal = _principal;
        } 

        public static IGenFormPrincipal GetPrincipal()
        {
            if (_principal != null) return _principal;

            SetPrincipal(new AnonymousIdentity());
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
