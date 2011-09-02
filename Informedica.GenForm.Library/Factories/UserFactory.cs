using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Users;

namespace Informedica.GenForm.Library.Factories
{
    public class UserFactory : EntityFactory<User, UserDto>
    {
        public UserFactory(UserDto dto) : base(dto)
        {
        }

        protected override User Create()
        {
            var user = User.Create(Dto);
            Add(user);
            return user;
        }
    }
}