using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.DataAccess.DataContexts;
using Informedica.GenForm.DataAccess.DataMappers;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Users;
using Informedica.GenForm.Library.Repositories;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class UserRepository : IRepository<IUser>
    {
        #region Implementation of IRepository<IUser>

        public IEnumerable<IUser> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IUser> GetByName(string name)
        {
            IList<IUser> list = new List<IUser>();

            using (var dataContext = DataContextFactory.CreateGenFormDataContext())
            {
                var users = FindUsersByName(dataContext, name);
                MapUsersToList(users, list);
            }

            return list;
        }

        private static IEnumerable<GenFormUser> FindUsersByName(GenFormDataContext dataContext, String name)
        {
            return dataContext.GenFormUser.Where(u => u.UserName == name);
        }

        private static void MapUsersToList(IEnumerable<GenFormUser> users, ICollection<IUser> list)
        {
            if (list == null) throw new ArgumentNullException("list");

            foreach (var genFormUser in users)
            {
                var user = User.NewUser();
                Mapper.MapFromDaoToBo(genFormUser, user);   
                list.Add(user);
            }
        }

        private static IDataMapper<IUser, GenFormUser> Mapper { get { return new UserMapper(); } }

        #endregion


    }
}
