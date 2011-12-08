using System.Collections;
using System.Data;
using System.Data.Common;
using System.Linq;
using Informedica.GenForm.Tests.RegressionTests;
using Informedica.GenForm.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Acceptance
{
    public class GenFormFixture
    {
        public int GetCountOfListOf(string type)
        {
            return new GenFormLists().GetListByType(type).Count;
        }

        public bool SetValidCredentialForUserWithName(string userName)
        {
            //ToDo: write testfixture
            return true;
        }

        public string GetFirstUserFromUserList()
        {
            return "system";
        }

        public string UserIsSystemUser(string userName)
        {
            return (userName == "system").ToString().ToLower();
        }

        public bool SetUserName(string userName)
        {
            // ToDo: write testfixture
            return userName.Length > 2 && userName.Length < 31 && UserExists(userName) != "true";
        }

        public bool SetPassword(string password)
        {
            //ToDo: write testcode
            return password.Length > 2 && password.Length < 21;
        }

        public bool InstallTestGenFormService()
        {
            return new SetupGenFormService().InstallTestGenformService();
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
            return (userName.ToLower().Trim() == "system" && password.Trim() == "Admin") ||
                   (userName.ToLower().Trim() == "admin" && password.Trim() == "Admin") ||
                   (userName.ToLower().Trim() == "foo" && password.Trim() == "baR");
        }

        public int GetUserCount()
        {
            return 1;
        }

        public string UserExists(string user)
        {
            //ToDo: write tests
            return (user.ToLower().Trim() == "admin" || user.ToLower().Trim() == "fred").ToString().ToLower();
        }

        public string GetEnvironments()
        {
            return "";
        }

        public string ConnectToServerAsUserWithPassword(string server, string user, string password)
        {
            return (server == "GenFormServer").ToString().ToLower();
        }

        public bool ListOfEnvironmentsContains(string environment)
        {
            // ToDo: write testfixture
            return (environment == "TestGenForm");
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
            return true;
        }

        public bool RegisterEnvironmentWithDatabase(string environment, string database)
        {
            return true;
        }

        public string GetDatabaseListForEnvironment(string environment)
        {
            return "";
        }

        public bool Login(string user)
        {
            return true;
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

    public class ValidPasswords
    {
        public string Password { get; set; }

        public bool IsValid()
        {
            return true;
        }
    }

    public class ValidUserNames
    {

        public string UserName { get; set; }
        public string ExistingUserName { get; set; }

        public bool IsValid()
        {
            return true;
        }

    }

    public class ValidInputForProductWithOneSubstance
    {
        public string GenericName { get; set; }
        public string BrandName { get; set; }
        public string ShapeName { get; set; }
        public string ProductQuantity { get; set; }
        public string ProductUnit { get; set; }
        public string PackageName { get; set; }
        public string SubstanceOrder { get; set; }
        public string SubstanceName { get; set; }
        public string SubstanceQuantity { get; set; }
        public string SubstanceQuantityUnit { get; set; }
        public string SubstanceConcentration { get; set; }
        public string ConcentrationUnit { get; set; }

        public bool IsValid()
        {
            return false;
        }

        public string ErrorMessage { get; set; }
    }

    public class SetupGenFormService
    {
        public bool InstallTestGenformService()
        {
            return true;
        }

    }

    public class ProductFixture
    {
        
    }

    public class ShapeFixture
    {
        
    }

    public class MyTestContext : TestContext
    {
        public override void WriteLine(string format, params object[] args)
        {
            throw new System.NotImplementedException();
        }

        public override void AddResultFile(string fileName)
        {
            throw new System.NotImplementedException();
        }

        public override void BeginTimer(string timerName)
        {
            throw new System.NotImplementedException();
        }

        public override void EndTimer(string timerName)
        {
            throw new System.NotImplementedException();
        }

        public override IDictionary Properties
        {
            get { throw new System.NotImplementedException(); }
        }

        public override DataRow DataRow
        {
            get { throw new System.NotImplementedException(); }
        }

        public override DbConnection DataConnection
        {
            get { throw new System.NotImplementedException(); }
        }
    }

    public class UserLogInScenario
    {
        public string ThenUserCanLogInWith(string userName, string password)
        {

            LoginAcceptanceTests.MyClassInitialize(new MyTestContext());
            var test = new LoginAcceptanceTests();

            try
            {
                test.MyTestInitialize();
                test.SystemUserCanLogin();
                return "true";

            }
            catch (System.Exception e)
            {
                return e.ToString();
            }
            finally
            {
                test.MyTestCleanup();
            }
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
