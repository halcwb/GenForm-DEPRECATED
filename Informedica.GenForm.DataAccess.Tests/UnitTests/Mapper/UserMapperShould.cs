using Informedica.GenForm.DataAccess.DataMappers;
using Informedica.GenForm.DataAccess.Tests.TestBase;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Mapper
{
    /// <summary>
    /// Summary description for UserMapperShould
    /// </summary>
    [TestClass]
    public class UserMapperShould: DataMapperTestBase<UserMapper, IUser, GenFormUser>
    {
        private const string Email = "halcwb@gmail.com";
        private const string FirstName = "Casper";
        private const string LastName = "Bollen";
        private const string Pager = "5019";
        private const string Password = "a secret";
        private const string UserName = "cbollen";
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void MapUserToDao()
        {
            FillUser();
            Mapper.MapFromBoToDao(Bo, Dao);
            AssertIsMapped();
        }

        [TestMethod]
        public void MapDaoToUser()
        {
            FillDao();
            Mapper.MapFromDaoToBo(Dao, Bo);
            AssertIsMapped();
        }

        private void FillDao()
        {
            Dao.Email = Email;
            Dao.FirstName = FirstName;
            Dao.LastName = LastName;
            Dao.PagerNumber = Pager;
            Dao.PassWord = Password;
            Dao.UserName = UserName;
        }

        private void FillUser()
        {
            Bo.Email = Email;
            Bo.FirstName = FirstName;
            Bo.LastName = LastName;
            Bo.Pager = Pager;
            Bo.Password = Password;
            Bo.UserName = UserName;
        }

        #region Overrides of DataMapperTestBase<UserMapper,IUser,GenFormUser>

        protected override bool IsMapped(IUser bo, GenFormUser dao)
        {
            var isMapped = bo.Email == dao.Email;
            isMapped = isMapped && bo.FirstName == dao.FirstName;
            isMapped = isMapped && bo.LastName == dao.LastName;
            isMapped = isMapped && bo.Pager == dao.PagerNumber;
            isMapped = isMapped && bo.Password == dao.PassWord;

            return isMapped;
        }

        #endregion
    }
}
