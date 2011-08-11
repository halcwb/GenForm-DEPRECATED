using System;
using Informedica.GenForm.Database;
using StructureMap.Configuration.DSL;

namespace Informedica.GenForm.Assembler.Assemblers
{
    public static class DatabaseAssembler
    {
        private static Registry _registry;

        public static Registry RegisterDependencies()
        {
            _registry = new Registry();

            _registry.For<GenFormDataContext>().Use<GenFormDataContext>();
            _registry.SelectConstructor(() => new GenFormDataContext(String.Empty));

            return _registry;
        }
    }
}
