using Informedica.GenForm.IoC;

namespace Informedica.GenForm.Assembler
{
    public static class GenFormApplication
    {
        public static void Initialize()
        {
            LoginAssembler.RegisterDependencies();
            ProductAssembler.RegisterDependencies();
            DatabaseAssembler.RegisterDependencies();

            ObjectFactory.Initialize();
        }
    }
}
