namespace Informedica.GenForm.Acceptance
{
    public class UserLogInScenario
    {
        public string ThenUserCanLogInWith(string userName, string password)
        {
            //ToDo Rewrite test
            return "";

        }

        public bool RegisterUserWith(string userName, string password)
        {
            return true;
        }

        public string EchoUser(string user)
        {
            return user;
        }

        public bool LogInUserWithPassword(string userName, string password)
        {
            var login = new UserLoginDecisions();
            login.GivenUser = userName;
            login.GivenPassword = password;
            return login.GivenUserLogsIn(userName);
        }

        public bool UserHasPassword(string userName, string password)
        {
            return LogInUserWithPassword(userName, password);
        }
    }
}