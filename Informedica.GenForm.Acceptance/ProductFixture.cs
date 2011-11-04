using Informedica.GenForm.TestFixtures.Fixtures;
using fit;

namespace Informedica.GenForm.Acceptance
{
    public class GenFormFixture
    {
        public bool InstallTestGenFormService()
        {
            return new SetupGenFormService().InstallTestGenformService();
        }

        public bool RegisterUserWith(string userName, string password)
        {
            return new UserLogInScenario().RegisterUserWith(userName, password);
        }

        public bool ThenUserCanLoginWith(string userName, string password)
        {
            return new UserLogInScenario().ThenUserCanLogInWith(userName, password);
        }
        public string EchoUser(string user)
        {
            return new UserLogInScenario().EchoUser(user);
        }

        public bool LogInUserWithPassword(string userName, string password)
        {
            return new UserLogInScenario().LogInUserWithPassword(userName, password);
        }

        public bool UserHasPassword(string userName, string password)
        {
            return new UserLogInScenario().UserHasPassword(userName, password);
        }

        public int GetUserCount()
        {
            return 1;
        }

        public bool UserExists(string user)
        {
            return true;
        }
    }

    public class SetupGenFormService
    {
        public bool InstallTestGenformService()
        {
            return true;
        }

    }

    public class ProductFixture: ColumnFixture
    {
        public new object GetTargetObject()
        {
            return ProductTestFixtures.GetDopamineDto();
        }
    }

    public class UserLogInScenario
    {
        public bool ThenUserCanLogInWith(string userName, string password)
        {
            var login =new UserLoginDecisions();
            login.GivenUser = userName;
            login.GivenPassword = password;
            return login.GivenUserLogsIn(userName);
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

    public class AdminUserLogin
    {
        public void SetUser(string user)
        {
               
        }

        public string UserLogsInWithPassword(string user, string password)
        {
            return "succesfull";
        }

        public string EchoUser(string user)
        {
            return user;
        }
    }
}
