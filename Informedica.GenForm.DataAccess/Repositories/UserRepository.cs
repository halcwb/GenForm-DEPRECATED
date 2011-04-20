using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Informedica.GenForm.Library.DataAccess;
using Informedica.GenForm.Library.DomainModel.Users;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class UserRepository: IRepository<IUser>
    {
        #region Implementation of IRepository<IUser>

        public IUser GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IUser GetByName(string name)
        {
            var user = User.NewUser();
            user.UserName = name;
            return user;
        }

        #endregion
    }
}
