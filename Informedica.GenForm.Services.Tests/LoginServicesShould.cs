using Informedica.GenForm.Assembler;
using Informedica.GenForm.Services.UserLogin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Services.Tests
{
    [TestClass]
    public class LoginServicesShould
    {
        [TestMethod]
        public void LoginUserAdminWithPasswordAdminInEnvironmentTestGenFormEnvironmentShouldReturnTrue()
        {
            GenFormApplication.Initialize();

            var dto = new UserLoginDto
                          {
                              UserName = "Admin",
                              Password = "Admin",
                              Environment = "TestGenForm"
                          };

            Assert.IsTrue(LoginServices.Login(dto));
        }
    }
}
