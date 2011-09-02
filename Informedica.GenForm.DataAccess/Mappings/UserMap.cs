using Informedica.GenForm.Library.DomainModel.Users;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class UserMap : EntityMap<User>
    {
        public UserMap()
        {
            Map(x => x.Email).Not.Nullable().Unique();
            Map(x => x.FirstName).Not.Nullable();
            Map(x => x.LastName).Not.Nullable();
            Map(x => x.Pager);
            Map(x => x.Password).Not.Nullable();
            
        }
    }
}
