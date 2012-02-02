namespace Informedica.GenForm.Settings
{
    public class SettingsSettingReader : SettingReader
    {
        public override string ReadSetting(string key)
        {
            return key == "GenForm" ? Properties.Settings.Default.GenForm : Properties.Settings.Default.GenFormTest;
        }
    }
}