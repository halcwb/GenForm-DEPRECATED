using System;

namespace Informedica.GenForm.Library.Security
{
    public class LoginCriteria: ILoginCriteria
    {
        #region Implementation of ILoginUser

        public string UserName { get; set; }

        public string Password { get; set; }
        #endregion

        #region Factory Methods

        public static ILoginCriteria NewLoginUser(String name, String password)
        {
            var user = new LoginCriteria {UserName = name, Password = password};

            return user;
        } 

        #endregion

    }
}
