using System;

namespace Informedica.GenForm.Library.Security
{
    class LoginUser: ILoginUser
    {
        #region Implementation of ILoginUser

        public string UserName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string Password
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        #endregion

        public static ILoginUser NewLoginUser(String name, String password)
        {
            var user = new LoginUser();
            user.UserName = name;
            user.Password = password;

            return user;
        }
    }
}
