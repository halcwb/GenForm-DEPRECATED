using System.Globalization;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Services.UserLogin;

namespace Informedica.GenForm.Acceptance.UserLogin
{
    public class CanUserLogin
    {
        public CanUserLogin()
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
                LoginServices.Login(userLogin);
                return LoginServices.IsLoggedIn(userLogin.UserName).ToString(CultureInfo.InvariantCulture);

            }
            catch (System.Exception e)
            {
                return e.ToString();
            }
        }
    }
}
