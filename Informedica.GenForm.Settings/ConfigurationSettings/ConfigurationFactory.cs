using System.Configuration;

namespace Informedica.GenForm.Settings.ConfigurationSettings
{
    public abstract class ConfigurationFactory
    {
        public abstract Configuration GetConfiguration();
    }
}