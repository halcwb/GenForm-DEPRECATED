using System;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Users;
using Informedica.GenForm.Library.Repositories;
using NHibernate;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class UserRepository : Informedica.DataAccess.Repositories.Repository<User, Guid>, IRepository<User>
    {
        public UserRepository(ISessionFactory factory) : base(factory) {}

        #region Implementation of IRepository<User>

        public User GetByName(string name)
        {
            return this.SingleOrDefault(u => u.Name == name);
        }

        #endregion
    }
}
