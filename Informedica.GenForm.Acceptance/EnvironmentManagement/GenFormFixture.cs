using System.Globalization;
using System.Linq;
using Informedica.GenForm.Acceptance.UserLogin;

namespace Informedica.GenForm.Acceptance.EnvironmentManagement
{
    public class GenFormFixture
    {
        public int GetCountOfListOf(string type)
        {
            // ToDo Rewrite test
            return 0;
            //return new GenFormLists().GetListByType(type).Count;
        }

        public bool SetValidCredentialForUserWithName(string userName)
        {
            //ToDo: write testfixture
            return true;
        }

        public string GetFirstUserFromUserList()
        {
            return string.Empty;
        }

        public string UserIsSystemUser(string userName)
        {
            return (userName == "system").ToString(CultureInfo.InvariantCulture).ToLower();
        }

        public bool SetUserName(string userName)
        {
            // ToDo: write testfixture
            return false;
        }

        public bool SetPassword(string password)
        {
            //ToDo: write testcode
            return false;
        }

        public bool InstallTestGenFormService()
        {
            return false;
        }

        public bool RegisterUserWith(string userName, string password)
        {
            //ToDo: write testfixture
            return UserHasPassword(userName, password);
        }

        public string ThenUserCanLoginWith(string userName, string password)
        {
            return new UserLogInScenario().ThenUserCanLogInWith(userName, password);
        }

        public string EchoUser(string user)
        {
            return new UserLogInScenario().EchoUser(user);
        }

        public bool LogInUserWithPassword(string userName, string password)
        {
            //ToDo: write testcode
            return UserHasPassword(userName, password);
        }

        public bool UserHasPassword(string userName, string password)
        {
            //ToDo: write testcode
            return false;
        }

        public int GetUserCount()
        {
            return 999999999;
        }

        public string UserExists(string user)
        {
            //ToDo: write tests
            return string.Empty;
        }

        public string GetEnvironments()
        {
            return "No environments";
        }

        public string ConnectToServerAsUserWithPassword(string server, string user, string password)
        {
            return string.Empty;
        }

        public bool ListOfEnvironmentsContains(string environment)
        {
            // ToDo: write testfixture
            return (environment == "kjhkjhkhkjhh");
        }

        public string Echo(string it)
        {
            return it;
        }

        public bool RegisterEnvironment(string environment)
        {
            return RegisterEnvironmentForServerWithLoginAndPassword(environment, "GenFormServer", "genformadmin",
                                                                    "Admin");
        }

        public bool RegisterEnvironmentForServerWithLoginAndPassword(string environment, string server, string login, string password)
        {
            return false;
        }

        public bool RegisterEnvironmentWithDatabase(string environment, string database)
        {
            return false;
        }

        public string GetDatabaseListForEnvironment(string environment)
        {
            return "";
        }

        public bool Login(string user)
        {
            return false;
        }

        public bool ListContains(string list, string search)
        {
            if (string.IsNullOrWhiteSpace(list) || string.IsNullOrWhiteSpace(search)) return false;

            var arryList = list.Split(',');
            return arryList.Any(item => item == search);
        }

        public bool AddDatabaseToEnvironment(string database, string environment)
        {
            return true;
        }

        public bool EnvironmentContainsDatabase(string environmnet, string database)
        {
            return true;
        }

        public bool UserCanAccessDatabaseInEnvironmet(string user, string database, string environment)
        {
            return true;
        }

        public bool LogInUserWithPasswordInDatabaseInEnvironment(string user, string password, string database, string environment)
        {
            return true;
        }

        public bool IsUserLoggedInDatabaseInEnvironment(string user, string database, string environment)
        {
            return true;
        }

        public bool RegisterUserWithPasswordInDatabaseInEnvironment(string user, string password, string database, string environmnet)
        {
            return true;
        }
    }
}