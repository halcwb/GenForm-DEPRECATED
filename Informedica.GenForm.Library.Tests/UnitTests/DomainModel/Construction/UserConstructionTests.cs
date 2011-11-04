using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Users;
using Informedica.GenForm.TestFixtures.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel.Construction
{
    [TestClass]
    public class UserConstructionTests
    {
        [TestMethod]
        public void ThatUserCanBeConstructedUsingValidDto()
        {
            var user = User.Create(UserTestFixtures.GetValidUserDto());
            Assert.IsNotNull(user);
            Assert.AreEqual(user.Name, UserTestFixtures.GetValidUserDto().Name);
        }

        [TestMethod]
        public void ThatUserCannotBeConstructedUsingAnInvalidDto()
        {
            try
            {
                User.Create(new UserDto());
                Assert.Fail();
            }
            catch (System.Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(AssertFailedException));
            }
        }
    }
}