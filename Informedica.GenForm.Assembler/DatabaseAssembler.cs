using System;
using Informedica.GenForm.Database;
using StructureMap.Configuration.DSL;

namespace Informedica.GenForm.Assembler
{
    public static class DatabaseAssembler
    {
        private static Boolean _hasBeenCalled;
        private static Registry _registry;

        public static Registry RegisterDependencies()
        {
            if (_hasBeenCalled) return _registry;
            _registry = new Registry();

            _registry.For<GenFormDataContext>().Use<GenFormDataContext>();
            _registry.SelectConstructor(() => new GenFormDataContext(String.Empty));

            _hasBeenCalled = true;
            return _registry;
        }
    }
}
