using System.Linq;
using Informedica.GenForm.Settings.Environments;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Acceptance.EnvironmentManagement
{
    public class AListOfEnvironmentsWillContain : GenFormEnvironmentFixture
    {
        private TestSource _source;
        private GenFormEnvironmentCollection _environments;
        private EnvironmentSettingsCollection _settings;
        private const string Seperator = ".";
        private const string SettingPlaceHolder = "Setting";

        private void Init()
        {
            _source = new TestSource();
            _settings = Isolate.Fake.Instance<EnvironmentSettingsCollection>();
            var col = new EnvironmentCollection(_settings);

            _environments = new GenFormEnvironmentCollection(col);
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
            return MachineName + Seperator + EnvironmentName + Seperator + SettingPlaceHolder + Seperator + Provider;
        }

        public string IsInTheList()
        {
            Init();
            return _environments.Any(e => e.MachineName == "MyMachine") ? "Yes" : "No";
        }
    }
}
