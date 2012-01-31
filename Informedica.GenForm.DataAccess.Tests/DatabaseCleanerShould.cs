using System.Linq;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.DataAccess.Databases;
using Informedica.GenForm.Library.DomainModel.Databases;
using Informedica.GenForm.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Linq;
using StructureMap;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using TestContext = Microsoft.VisualStudio.TestTools.UnitTesting.TestContext;

namespace Informedica.GenForm.DataAccess.Tests
{
    /// <summary>
    /// Summary description for DatabaseCleanerShould
    /// </summary>
    [TestClass]
    public class DatabaseCleanerShould: TestSessionContext
    {
        public DatabaseCleanerShould() : base(true)
        {
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            GenFormApplication.Initialize();
            ObjectFactory.Inject(typeof(IDatabaseConfig), new MsSql2008Config());
        }
        
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void StartWithAnEmptyDatabase()
        {
            var session = Context.CurrentSession();
            Assert.AreEqual(0, session.Query<EmptyDatabase>().Count());
            
        }

        // ToDo: Refactor this ugly test.
        [TestMethod]
        public void BeAbleToClearTheEmptyDatabase()
        {
            ConfigureLog4NetSmtp();
            var logger = GetLogger();
            
            var session = Context.CurrentSession();
            var emptyDb = new EmptyDatabase { IsEmpty = false };
            try
            {
                session.Save(emptyDb);

                Assert.AreEqual(1, session.Query<EmptyDatabase>().Count());
            }
            catch (System.Exception e)
            {
                logger.Fatal(e);
                Assert.Fail(e.ToString());
            }

            try
            {
                DatabaseCleaner.CleanDataBase(session);

                Assert.AreEqual(0, session.Query<EmptyDatabase>().Count());

            }
            catch (System.Exception e)
            {
                logger.Fatal(e);
                Assert.Fail(e.ToString());
            }
        }

        private static ILog GetLogger()
        {
            var logger = LogManager.GetLogger(typeof (DatabaseCleanerShould));
            return logger;
        }

        private static void ConfigureLog4NetSmtp()
        {
            var smtp = GetSmtpAppender();
            BasicConfigurator.Configure(smtp);
        }

        private static SmtpAppender GetSmtpAppender()
        {
            var smtp = new SmtpAppender
                           {
                               Name = "GMail",
                               Username = "informedica.genform@gmail.com",
                               Password = "hlab27jra",
                               SmtpHost = "smtp.gmail.com",
                               From = "halcwb@gmail.com",
                               To = "informedica.genform@gmail.com",
                               EnableSsl = true,
                               Port = 587,
                               Authentication = SmtpAppender.SmtpAuthentication.Basic,
                               Subject = "log4net",
                               BufferSize = 512,
                               Lossy = true,
                               Evaluator = new LevelEvaluator(Level.Fatal),
                               Layout =
                                   new PatternLayout(
                                   "%newline%date [%thread] %-5level %logger [%property{NDC}] - %message%newline%newline%newline")
                           };
            return smtp;
        }
    }
}