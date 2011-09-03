using System;
using System.Security;
using System.Security.Principal;
using System.Threading;
using Informedica.GenForm.Library.DomainModel.Users;
using Informedica.GenForm.Library.Services.Users;

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
            if (!CheckPassword(oldPassword)) throw new SecurityException("paswoord klopt niet");

            var user = GetCurrentUser();
            user.Password = newPassword;
        }

        public bool CheckPassword(string password)
        {
            var user = GetCurrentUser();
            return user.Password == password;
        }

        private static IUser GetCurrentUser()
        {
            return UserServices.GetUserByName(Thread.CurrentPrincipal.Identity.Name);
        }

        #endregion
    }
}
