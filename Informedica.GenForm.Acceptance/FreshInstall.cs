using System.Linq;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.Services.Users;
using Informedica.GenForm.Services.UserLogin;
using Informedica.GenForm.Tests;
using NHibernate.Context;

namespace Informedica.GenForm.Acceptance
{
    public class FreshInstall : TestSessionContext
    {
        public FreshInstall() : base(true)
        {
            GenFormApplication.Initialize();
        }

        public bool InstallNewGenformService()
        {
            try
            {
                MyTestInitialize();

                return CurrentSessionContext.HasBind(SessionFactory);
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public int GetUserCount()
        {
            UserServices.ConfigureSystemUser();

            return UserServices.Users.Count();
        }

        public string GetFirstUserFromUserList()
        {
            return UserServices.Users.First().UserName;
        }

        public bool UserIsSystemUser(string userName)
        {
            return GetFirstUserFromUserList() == userName;
        }

        public bool LogInUserWithPassword(string user, string password)
        {
            LoginServices.Login(new UserLoginDto
                                    {
                                        Environment = "TestGenForm",
                                        Password = password,
                                        UserName = user
                                    });

            return LoginServices.IsLoggedIn(user);
        }

        public bool UserHasPassword(string user, string password)
        {
            return  LoginServices.UserHasPassword(new UserLoginDto
                                              {
                                                  Environment = "",
                                                  Password = password, 
                                                  UserName = user
                                              });
        }

        public bool TearDownDatabase() 
        {
            try
            {
                MyTestCleanup();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            } 
        }

    }
}
