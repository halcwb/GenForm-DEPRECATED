using System.Collections.Generic;

namespace Informedica.GenForm.Assembler
{
    public static class ProviderCollector
    {
        public static IEnumerable<RegisteredProvider> GetRegisteredProviders()
        {
            return new List<RegisteredProvider>
                       {
                           new RegisteredProvider("SqLite")
                       };
        }
    }
}