using System;
using Informedica.GenForm.Presentation.Forms;

namespace Informedica.GenForm.Presentation.Security
{
    public class LoginForm : ILoginForm
    {
        #region Implementation of ILoginForm

        private readonly IFormField _userName = TextField.NewTextField();

        private readonly IFormField _password = TextField.NewTextField();

        private readonly IButton _login = Button.NewButton();

        public IFormField UserName
        {
            get { return _userName; }
        }

        public IFormField Password
        {
            get { return _password; }
        }

        public IButton Login
        {
            get { return _login; }
        }

        #endregion

        #region LoginForm Factory

        private  LoginForm() {}

        public  static ILoginForm NewLoginForm(String username, String password)
        {
            var form = new LoginForm();
            form._userName.Value = username;
            form._password.Value = password;
            form._login.Enabled = UserNameAndPasswordAreNotEmpty(username, password);

            return form;
        }

        private static bool UserNameAndPasswordAreNotEmpty(string username, string password)
        {
            return !String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password);
        }

        #endregion
    }
}