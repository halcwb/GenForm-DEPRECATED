using StructureMap;

namespace Informedica.GenForm.Assembler
{
    public static class GenFormApplication
    {
        public static void Initialize()
        {
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry(DatabaseAssembler.RegisterDependencies());
                x.AddRegistry(ProductAssembler.RegisterDependencies());
            });
            LoginAssembler.RegisterDependencies();
        }
    }
}
