using System.Globalization;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.Services.Users;
using Informedica.GenForm.Services.Environments;
using Informedica.GenForm.Services.UserLogin;
using Informedica.GenForm.Tests;

namespace Informedica.GenForm.Acceptance.UserLogin
{
    public class CanUserLogin : TestSessionContext
    {
        public CanUserLogin(): base(true)
        {
            GenFormApplication.Initialize();
        }

        public string Environment { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public string CanLogin()
        {
            var userLogin = new UserLoginDto
                                {
                                    UserName = User,
                                    Password = Password,
                                    Environment = Environment
                                };

            try
            {
                MyTestInitialize();
                EnvironmentServices.SetEnvironment(Environment);
                UserServices.ConfigureSystemUser();
                LoginServices.Login(userLogin);
                var result = LoginServices.IsLoggedIn(userLogin.UserName).ToString(CultureInfo.InvariantCulture);
                MyTestCleanup();
                
                return result;
            }
            catch (System.Exception)
            {
                return "False";
            }
        }
    }
}
