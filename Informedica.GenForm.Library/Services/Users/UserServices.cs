using System;
using System.Collections.Generic;
using System.Threading;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Users;
using Informedica.GenForm.Library.Factories;

namespace Informedica.GenForm.Library.Services.Users
{
    public class UserServices : ServicesBase<User, UserDto>
    {
        private static UserServices _instance;
        private static readonly object LockThis = new object();

        private static UserServices Instance
        {
            get
            {
                if (_instance == null)
                    lock (LockThis)
                    {
                        if (_instance == null)
                        {
                            var instance = new UserServices();
                            Thread.MemoryBarrier();
                            _instance = instance;
                        }
                    }
                return _instance;
            }
        }

        public static UserFactory WithDto(UserDto dto)
        {
            return (UserFactory)Instance.GetFactory(dto);
        }

        public static User GetUserById(Guid id)
        {
            return Instance.GetById(id);
        }

        public static User GetUserByName(String name)
        {
            return Instance.GetByName(name);
        }

        public static IEnumerable<User> Users
        {
            get { return Instance.Repository; }
        }

        public static void Delete(User user)
        {
            Instance.Repository.Remove(user);
        }
    }
}
