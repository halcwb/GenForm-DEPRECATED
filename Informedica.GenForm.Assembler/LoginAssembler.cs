﻿using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Library.DataAccess;
using Informedica.GenForm.Library.DomainModel.Users;
using Informedica.GenForm.Library.ServiceProviders;

namespace Informedica.GenForm.Assembler
{
    public class LoginAssembler
    {
        public static void RegisterDependencies()
        {
            var repository = (IRepository<IUser>)new UserRepository();
            DalServiceProvider.Instance.RegisterInstanceOfType(repository);
        }
    }
}
