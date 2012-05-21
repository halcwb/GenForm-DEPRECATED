using Informedica.GenForm.Library.Security;
using Informedica.GenForm.Services.Environments;

namespace Informedica.GenForm.Services.UserLogin
{
    public class LoginServices
    {
        public static void Login(UserLoginDto dto)
        {
            EnvironmentServices.SetEnvironment(dto.Environment);

            var criteria = new LoginCriteria
                               {
                                   UserName = dto.UserName,
                                   Password = dto.Password
                               };
            GenFormPrincipal.Login(criteria);
        }

        public static bool IsLoggedIn(string dto)
        {
            throw new System.NotImplementedException();
        }

        public static string GetLoggedIn()
        {
            throw new System.NotImplementedException();
        }

        public static void ChangePassword(UserLoginDto loginCriteria, string newPassword)
        {
            throw new System.NotImplementedException();
        }

        public static bool CheckPassword(string newPassword)
        {
            throw new System.NotImplementedException();
        }
    }
}