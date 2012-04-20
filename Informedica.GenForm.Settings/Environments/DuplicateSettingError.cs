using System;

namespace Informedica.GenForm.Settings.Environments
{
    public class DuplicateSettingError : Exception
    {
        public DuplicateSettingError(string message): base(message) {}
    }
}