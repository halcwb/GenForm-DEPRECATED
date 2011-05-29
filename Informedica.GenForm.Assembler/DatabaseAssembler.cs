using System;
using Informedica.GenForm.Database;
using Informedica.GenForm.IoC.Registries;

namespace Informedica.GenForm.Assembler
{
    public static class DatabaseAssembler
    {
        private static Boolean _hasBeenCalled;

        public static void RegisterDependencies()
        {
            if (_hasBeenCalled) return;
            
            DatabaseRegistry.RegisterTypeFor<GenFormDataContext, GenFormDataContext>();

            _hasBeenCalled = true;
        }
    }
}
