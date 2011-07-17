using Informedica.GenForm.Library.DomainModel.Databases;

namespace Informedica.GenForm.DataAccess.Databases
{
    public class DatabaseSetting : IDatabaseSetting
    {
        #region Implementation of IDatabase

        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public string Machine { get; set; }

        #endregion
    }
}