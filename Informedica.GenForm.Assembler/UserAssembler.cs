using Informedica.GenForm.Library.DomainModel.Users;
using StructureMap.Configuration.DSL;

namespace Informedica.GenForm.Assembler
{
    public static class UserAssembler
    {
        private static Registry _registry;

        public static Registry RegisterDependencies()
        {
            _registry = new Registry();

            _registry.For<IUser>().Use<User>();
            return _registry;
        }

    }
}
