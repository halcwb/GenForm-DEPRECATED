namespace Informedica.GenForm.Assembler
{
    public static class GenFormApplication
    {
        public static void Initialize()
        {
            LoginAssembler.RegisterDependencies();
        }
    }
}
