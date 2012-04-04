namespace Informedica.GenForm.Assembler
{
    public class RegisteredProvider
    {
        public RegisteredProvider(string providerName)
        {
            ProviderName = providerName;
        }

        public string ProviderName { get; private set; }
    }
}