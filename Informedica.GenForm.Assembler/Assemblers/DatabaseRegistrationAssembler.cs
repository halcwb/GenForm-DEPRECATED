using System;
using Informedica.GenForm.DataAccess.Databases;
using Informedica.GenForm.Library.DomainModel.Databases;
using StructureMap.Configuration.DSL;
using Environment = Informedica.GenForm.DataAccess.Databases.Environment;

namespace Informedica.GenForm.Assembler.Assemblers
{
    class DatabaseRegistrationAssembler
    {
        private static Boolean _hasBeenCalled;
        private static Registry _registry;

        public static Registry RegisterDependencies()
        {
            if (_hasBeenCalled) return _registry;
            _registry = new Registry();

            _registry.For<IEnvironment>().Use<Environment>();
            _registry.For<IDatabaseConnection>().Use<DatabaseConnection>();

            _hasBeenCalled = true;
            return _registry;
        }
    }
}
