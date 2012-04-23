using System.Linq;
using Informedica.GenForm.Settings.Environments;
using Informedica.SecureSettings.Sources;

namespace Informedica.GenForm.Acceptance.EnvironmentManagement
{
    public class AListOfEnvironmentsWillContain : GenFormEnvironmentFixture
    {
        private TestSource _source;
        private GenFormEnvironmentCollection _environments;
        private const string Seperator = ".";
        private const string SettingPlaceHolder = "Setting";

        public AListOfEnvironmentsWillContain()
        {
            Init();
        }

        private void Init()
        {
            _source = new TestSource();
            _source.WriteSetting(new Setting(GetDatabaseSettingName(), ConnectionString, "Conn", false));
            _source.WriteSetting(new Setting(GetLogPathSettingName(), LogPath, "Conn", false));
            _source.WriteSetting(new Setting(GetExportPathSettingName(), LogPath, "Conn", false));

            _environments = new GenFormEnvironmentCollection();
        }

        private string GetDatabaseSettingName()
        {
            return GetSettingName().Replace(SettingPlaceHolder, GenFormEnvironment.Settings.Database.ToString());
        }

        private string GetLogPathSettingName()
        {
            return GetSettingName().Replace(SettingPlaceHolder, GenFormEnvironment.Settings.LogPath.ToString());
        }

        private string GetExportPathSettingName()
        {
            return GetSettingName().Replace(SettingPlaceHolder, GenFormEnvironment.Settings.ExportPath.ToString());
        }

        private string GetSettingName()
        {
            return MachineName + Seperator + EnvironmentName + SettingPlaceHolder + Provider;
        }

        public string IsInTheList()
        {
            return _environments.Any(e => e.MachineName == MachineName && e.Name == Name) ? "Yes" : "No";
        }
    }
}
