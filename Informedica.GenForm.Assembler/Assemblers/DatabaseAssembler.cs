using System;
using Informedica.GenForm.Database;
using NHibernate;
using StructureMap.Configuration.DSL;

namespace Informedica.GenForm.Assembler.Assemblers
{
    public static class DatabaseAssembler
    {
        private static Registry _registry;

        public static Registry RegisterDependencies()
        {
            _registry = new Registry();
            // ToDo: rewrite dependencies on L2Sql to NH
            _registry.For<GenFormDataContext>().Use<GenFormDataContext>();
            _registry.SelectConstructor(() => new GenFormDataContext(String.Empty));

            _registry.For<ISessionFactory>().Use(GenFormApplication.SessionFactory);

            return _registry;
        }
    }
}
