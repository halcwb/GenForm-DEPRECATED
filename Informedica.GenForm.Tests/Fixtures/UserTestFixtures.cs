using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Users;

namespace Informedica.GenForm.Tests.Fixtures
{
    public static class UserTestFixtures
    {
        public static UserDto GetValidUserDto()
        {
            return new UserDto
                       {
                           Email = "foo@gmail.com",
                           FirstName = "Foo",
                           LastName = "Bar",
                           Name = "foobar",
                           Pager = "12345",
                           Password = "secret"
                       };
        }

        public static User CreateFooBarUser()
        {
            return User.Create(GetValidUserDto());
        }
    }
}