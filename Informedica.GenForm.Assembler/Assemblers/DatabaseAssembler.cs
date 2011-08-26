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
            _registry.For<ISessionFactory>().Use(GenFormApplication.SessionFactory);

            return _registry;
        }
    }
}
