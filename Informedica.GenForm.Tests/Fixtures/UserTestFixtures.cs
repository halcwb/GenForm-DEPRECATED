using Informedica.GenForm.Library.DomainModel.Data;

namespace Informedica.GenForm.Tests.Fixtures
{
    public static class UserTestFixtures
    {
        public static UserDto GetValidUserDto()
        {
            return new UserDto
                       {
                           Email = "admin@gmail.com",
                           FirstName = "Admin",
                           LastName = "Admin",
                           Name = "Admin",
                           Pager = "12345",
                           Password = "secret"
                       };
        }
    }
}