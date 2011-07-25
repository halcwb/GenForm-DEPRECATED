using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Library.DomainModel.Users;
using Informedica.GenForm.Library.Repositories;
using Informedica.GenForm.Library.ServiceProviders;
using StructureMap.Configuration.DSL;

namespace Informedica.GenForm.Assembler
{
    public static class UserAssembler
    {
        private static Registry _registry;

        public static void SetupServiceProvider()
        {
            var repository = (IRepository<IUser>)new UserRepository();
            DalServiceProvider.Instance.RegisterInstanceOfType(repository);

        }

        public static Registry RegisterDependencies()
        {
            _registry = new Registry();

            _registry.For<IUser>().Use<User>();
            return _registry;
        }

    }
}
