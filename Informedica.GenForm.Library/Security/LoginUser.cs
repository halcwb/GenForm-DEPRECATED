using System;

namespace Informedica.GenForm.Library.Security
{
    public class LoginUser: ILoginUser
    {
        #region Implementation of ILoginUser

        public string UserName { get; set; }

        public string Password { get; set; }
        #endregion

        #region Factory Methods

        private LoginUser() {}

        public static ILoginUser NewLoginUser(String name, String password)
        {
            var user = new LoginUser();
            user.UserName = name;
            user.Password = password;

            return user;
        } 

        #endregion
    }
}
