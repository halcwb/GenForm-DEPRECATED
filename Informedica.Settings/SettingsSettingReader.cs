namespace Informedica.Settings
{
    public class SettingsSettingReader : SettingReader
    {
        public override string ReadSetting(string key)
        {
            return Properties.Settings.Default.GenFormTest;
        }
    }
}