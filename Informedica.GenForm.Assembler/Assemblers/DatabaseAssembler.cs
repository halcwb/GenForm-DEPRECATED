using Informedica.DataAccess.Databases;
using StructureMap.Configuration.DSL;

namespace Informedica.GenForm.Assembler.Assemblers
{
    public static class DatabaseAssembler
    {
        private static Registry _registry;

        public static Registry RegisterDependencies()
        {
            _registry = new Registry();
            _registry.For<IDatabaseConfig>().Use(new SqlLiteConfig());
            
            return _registry;
        }
    }
}
