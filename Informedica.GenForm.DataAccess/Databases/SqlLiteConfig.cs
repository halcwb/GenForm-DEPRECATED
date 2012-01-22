using FluentNHibernate.Cfg.Db;

namespace Informedica.GenForm.DataAccess.Databases
{
    public class SqlLiteConfig: IDatabaseConfig
    {
        public IPersistenceConfigurer Configurer(string connectString)
        {
            return SQLiteConfiguration.Standard.InMemory();
        }
    }
}
