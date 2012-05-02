using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Settings.ConfigurationSettings;
using Informedica.GenForm.Settings.Environments;
using Informedica.SecureSettings.Sources;

namespace Informedica.GenForm.Acceptance.EnvironmentManagement
{
    public class CurrentEnvironmentsReporter
    {
        private EnvironmentCollection _environments;
        private GenFormEnvironmentCollection _genformEnvs;
        private ICollection<EnvironmentSetting> _settings; 
        private ICollection<ISetting> _source;

        public void Init()
        {
            _source = SettingSourceFactory.GetSettingSource();
            _settings = new EnvironmentSettingsCollection(_source);
            _environments = new EnvironmentCollection(_settings);
            _genformEnvs = new GenFormEnvironmentCollection(_environments);
        }

        public string GetTheCurrentListOf(string items)
        {
            try
            {
                Init();

                if (items == "settings") return GetListOfSettings();
                if (items == "environments") return GetListOfEnvironments();
                if (items == "genform environments") return GetListOfGenFormEnvironments();

                return "nothing";

            }
            catch (System.Exception e)
            {
                return e.ToString();
            }
        }

        private string GetListOfGenFormEnvironments()
        {
            string result = string.Empty;
            foreach (var environment in _genformEnvs)
            {
                result += "=== MACHINE = " + environment.MachineName + 
                          ": NAME = " + environment.Name + 
                          ": Database = " + environment.Database + 
                          ": LogPath = " + environment.LogPath + 
                          ": ExportPath = " + environment.ExportPath + " \n";
            }

            return string.IsNullOrWhiteSpace(result) ? "empty" : result;
        }

        private string GetListOfEnvironments()
        {
            string result = string.Empty;
            foreach (var environment in _environments)
            {
                result += "=== MACHINE = " + environment.MachineName + ": NAME = " + environment.Name + "Settings Count = " + environment.Settings.Count() + " \n";
            }

            return string.IsNullOrWhiteSpace(result) ? "empty" : result;
        }

        private string GetListOfSettings()
        {
            string result = string.Empty;
            foreach (var setting in _source)
            {
                result += "=== KEY = " + setting.Key + ": VALUE = " + setting.Value + " \n";
            }

            return string.IsNullOrWhiteSpace(result) ? "empty" : result;
        }
    }
}
