using System;
using Informedica.GenForm.Library.DomainModel.Users.Interfaces;
using Informedica.GenForm.Library.Security;
using Informedica.GenForm.Library.Services.Users;
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
        private UserLoginDto _dto;
        private GenFormPrincipal _principal;
        private IGenFormIdentity _identity;

        [TestInitialize]
        public void Init()
        {
            _principal = Isolate.Fake.Instance<GenFormPrincipal>();
            _identity = Isolate.Fake.Instance<IGenFormIdentity>();
            Isolate.WhenCalled(() => _principal.Identity).WillReturn(_identity);
            
            Isolate.WhenCalled(() => GenFormPrincipal.GetPrincipal()).WillReturn(_principal);
            Isolate.WhenCalled(() => GenFormPrincipal.Login(_criteria)).IgnoreCall();

            Isolate.WhenCalled(() => EnvironmentServices.SetEnvironment(string.Empty)).IgnoreCall();

            _dto = GetAdminUserDto();
        }

        [Isolated]
        [TestMethod]
        public void FirstSetTheEnvironmentWhenLogginIn()
        {

            try
            {
                LoginServices.Login(_dto);
                var env = _dto.Environment;
                Isolate.Verify.WasCalledWithAnyArguments(() => EnvironmentServices.SetEnvironment(env));
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [Isolated]
        [TestMethod]
        public void UseGenFormPrincipalToLogin()
        {
            try
            {
                LoginServices.Login(_dto);
                _criteria = new LoginCriteria
                                {
                                    UserName = _dto.UserName,
                                    Password = _dto.Password
                                };

                Isolate.Verify.WasCalledWithAnyArguments(() => GenFormPrincipal.Login(_criteria));
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [Isolated]
        [TestMethod]
        public void ReturnTrueWhenValidUserLogin()
        {
            Isolate.WhenCalled(() => _principal.IsLoggedIn()).WillReturn(true);
            Isolate.WhenCalled(() => _identity.Name).WillReturn(_dto.UserName);

            Assert.IsTrue(LoginServices.IsLoggedIn(_dto.UserName));
        }

        [Isolated]
        [TestMethod]
        public void ReturnFalseWhenInvalidUserLogin()
        {
            Isolate.WhenCalled(() => _principal.IsLoggedIn()).WillReturn(false);
            Isolate.WhenCalled(() => _identity.Name).WillReturn(_dto.UserName);


            Assert.IsFalse(LoginServices.IsLoggedIn(_dto.UserName));
        }

        [Isolated]
        [TestMethod]
        public void ReturnFalseWhenValidUserLoginButDifferentUser()
        {
            Isolate.WhenCalled(() => _principal.IsLoggedIn()).WillReturn(true);
            Isolate.WhenCalled(() => _identity.Name).WillReturn("John");

            Assert.IsFalse(LoginServices.IsLoggedIn(_dto.UserName));
        }

        [Isolated]
        [TestMethod]
        public void ReturnFalseWhenAdminUserHasNotPasswordFoo()
        {
            IsolateUserHasPasswordMethod();

            _dto.Password = "Foo";
            Assert.IsFalse(LoginServices.UserHasPassword(_dto));
        }

        [Isolated]
        [TestMethod]
        public void ReturnTrueWhenAdminUserHasPasswordAdmin()
        {
            IsolateUserHasPasswordMethod();

            Assert.IsTrue(LoginServices.UserHasPassword(GetAdminUserDto()));
        }


        private void IsolateUserHasPasswordMethod()
        {
            _dto = GetAdminUserDto();
            var user = Isolate.Fake.Instance<IUser>();
            var password = _dto.Password;
            var userName = _dto.UserName;
            Isolate.WhenCalled(() => user.Password).WillReturn(password);
            Isolate.WhenCalled(() => UserServices.GetUserByName(userName)).WillReturn(user);
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
