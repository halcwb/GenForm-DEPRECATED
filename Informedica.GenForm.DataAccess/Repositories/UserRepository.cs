using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Users;
using NHibernate;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class UserRepository : NHibernateRepository<User>
    {
        private static readonly UserComparer Comparer = new UserComparer(); 

        public UserRepository(ISessionFactory factory) : base(factory) {}

        public override void Add(User item)
        {
            Add(item, Comparer);
        }
    }
}
