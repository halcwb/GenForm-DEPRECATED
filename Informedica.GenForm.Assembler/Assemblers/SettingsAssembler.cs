using Informedica.GenForm.Settings;
using StructureMap.Configuration.DSL;

namespace Informedica.GenForm.Assembler.Assemblers
{
    public class SettingsAssembler
    {
        private static Registry _registry;

        public static Registry RegisterDependencies()
        {
            _registry = new Registry();

            _registry.For<SettingReader>().Use(new SettingsSettingReader());

            return _registry;
        }
    }
}