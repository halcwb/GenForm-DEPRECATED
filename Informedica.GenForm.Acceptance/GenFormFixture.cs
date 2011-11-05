using System.Collections;
using System.Data;
using System.Data.Common;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Tests.RegressionTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using StructureMap;

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

        public string UserExists(string user)
        {
            return new UserLogInScenario().ThenUserCanLogInWith("admin", "Admin");
        }
    }

    public class SetupGenFormService
    {
        public bool InstallTestGenformService()
        {
            return true;
        }

    }

    public class MyTestContext: TestContext
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
