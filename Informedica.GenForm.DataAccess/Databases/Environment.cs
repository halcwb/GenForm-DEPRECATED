using Informedica.GenForm.Library.DomainModel.Databases;

namespace Informedica.GenForm.DataAccess.Databases
{
    public class Environment : IEnvironment
    {
        #region Implementation of IDatabase

        public string Name { get; set; }
        public string ConnectionString { get; set; }

        #endregion
    }
}