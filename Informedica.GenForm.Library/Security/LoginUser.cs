using System;

namespace Informedica.GenForm.Library.Security
{
    public class LoginUser: ILoginCriteria
    {
        #region Implementation of ILoginUser

        public string UserName { get; set; }

        public string Password { get; set; }
        #endregion

        #region Factory Methods

        public static ILoginCriteria NewLoginUser(String name, String password)
        {
            var user = new LoginUser {UserName = name, Password = password};

            return user;
        } 

        #endregion

        public static ILoginCriteria NewLoginUser(string admin, string password, string environment)
        {
            var user = new LoginUser {UserName = admin, Password = password, Environment = environment};
            return user;
        }

        public string Environment { get; set; }
    }
}
