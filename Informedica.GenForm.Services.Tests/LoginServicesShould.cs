using System;
using Informedica.GenForm.Library.Security;
using Informedica.GenForm.Services.Environments;
using Informedica.GenForm.Services.UserLogin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Services.Tests
{
    [TestClass]
    public class LoginServicesShould
    {
        private LoginCriteria _criteria;

        [TestInitialize]
        public void Init()
        {
            Isolate.WhenCalled(() => GenFormPrincipal.Login(_criteria)).IgnoreCall();
            Isolate.WhenCalled(() => EnvironmentServices.SetEnvironment(string.Empty)).IgnoreCall();
        }

        [TestMethod]
        public void FirstSetTheEnvironmentWhenLogginIn()
        {
            var dto = GetAdminUserDto();

            try
            {
                LoginServices.Login(dto);
                var env = dto.Environment;
                Isolate.Verify.WasCalledWithAnyArguments(() => EnvironmentServices.SetEnvironment(env));
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void UseGenFormPrincipalToLogin()
        {
            var dto = GetAdminUserDto();
            try
            {
                LoginServices.Login(dto);
                _criteria = new LoginCriteria
                                {
                                    UserName = dto.UserName,
                                    Password = dto.Password
                                };

                Isolate.Verify.WasCalledWithAnyArguments(() => GenFormPrincipal.Login(_criteria));
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        private static UserLoginDto GetAdminUserDto()
        {
            var dto = new UserLoginDto
                          {
                              UserName = "Admin",
                              Password = "Admin",
                              Environment = "TestGenForm"
                          };
            return dto;
        }
    }
}
