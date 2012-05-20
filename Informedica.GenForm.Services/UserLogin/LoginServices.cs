namespace Informedica.GenForm.Services.UserLogin
{
    public class LoginServices
    {
        public static bool Login(UserLoginDto dto)
        {
            var criteria = new Library.Security.LoginUser
                               {
                                   UserName = dto.UserName,
                                   Password = dto.Password,
                                   Environment = dto.Environment
                               };

            Library.Services.Users.LoginServices.Login(criteria);
            return Library.Services.Users.LoginServices.IsLoggedIn(criteria);
        }
    }
}