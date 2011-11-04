using Informedica.GenForm.Assembler;
using Informedica.GenForm.Assembler.Contexts;
using Informedica.GenForm.Library.Security;
using Informedica.GenForm.Library.Services.Users;
using Informedica.GenForm.Tests;
using NHibernate;
using StructureMap;

namespace Informedica.GenForm.Acceptance
{
    public class UserLoginDecisions
    {
        private static bool _loggedIn;

        public UserLoginDecisions()
        {
            GenFormApplication.Initialize();
            ObjectFactory.Configure(x => x.For<ISessionFactory>().Use(GenFormApplication.SessionFactory));
            DatabaseCleaner.CleanDataBase();
        }

        public string GivenUser { get; set; }

        public string GivenPassword { get; set; }

        public string GivenEnvironment { get; set; }

        public string GivenFormularium { get; set; }

        public bool IsUserAuthenticated()
        {
            using (new SessionContext())
            {
                var user = LoginUser.NewLoginUser(GivenUser, GivenPassword);
                LoginServices.Login(user);
                return LoginServices.IsLoggedIn(user);
                
            }
        }

        public bool IsUserLoggedIn()
        {
            return _loggedIn;
        }

        public bool GivenUserLogsIn(string user)
        {
            _loggedIn = user == "Admin";
            return _loggedIn;
        }

        public bool IsLoggedInUser(string user)
        {
            return _loggedIn && user == "Admin";
        }

        public bool IsUserAuthorized()
        {
            return GivenUser == "Admin";
        }

        public bool CanUserLogin()
        {
            return IsUserAuthenticated() && IsUserAuthorized();
        }

    }
}
