using System;

namespace Informedica.GenForm.Settings
{
    public class SettingsManagerException : Exception
    {
        public SettingsManagerException(string file, Exception exception) : base("Could not create: " + file + " throws error:" + exception)
        {
            
        }
    }
}