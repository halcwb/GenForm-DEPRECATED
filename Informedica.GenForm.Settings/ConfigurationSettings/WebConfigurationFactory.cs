using System.Configuration;
using System.Web.Configuration;

namespace Informedica.GenForm.Settings.ConfigurationSettings
{
    public class WebConfigurationFactory : ConfigurationFactory
    {
        #region Overrides of ConfigurationFactory

        public override Configuration GetConfiguration()
        {
            return WebConfigurationManager.OpenWebConfiguration("/GenForm");
        }

        #endregion
    }
}