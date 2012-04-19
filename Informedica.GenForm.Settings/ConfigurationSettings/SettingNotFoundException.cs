using System;

namespace Informedica.GenForm.Settings.ConfigurationSettings
{
    public class SettingNotFoundException: Exception
    {
        public SettingNotFoundException(string name): base(name) {}
    }
}