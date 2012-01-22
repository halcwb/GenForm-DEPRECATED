using FluentNHibernate.Cfg.Db;

namespace Informedica.GenForm.DataAccess.Databases
{
    public class MsSql2008Config : IDatabaseConfig
    {
        public IPersistenceConfigurer Configurer(string connectString)
        {
            return MsSqlConfiguration.MsSql2008.ShowSql().AdoNetBatchSize(5).MaxFetchDepth(2).ConnectionString(connectString);
        }
    }
}