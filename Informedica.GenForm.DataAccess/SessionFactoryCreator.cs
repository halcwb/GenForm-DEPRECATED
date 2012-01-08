using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Informedica.GenForm.DataAccess.Databases;
using Informedica.Settings;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Informedica.GenForm.DataAccess
{
    public static class SessionFactoryCreator
    {
        private static readonly string ExportPath = SettingsManager.Instance.GetExporthPath();
        private static readonly string LogPath = SettingsManager.Instance.GetLogPath();

        static SessionFactoryCreator()
        {
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
        }

        [Obsolete]
        public static ISessionFactory CreateSessionFactory()
        {
            return CreateSessionFactory("GenFormTest");
        }

        private static void BuildSchema(Configuration config)
        {
            // first drop the database to recreate a new one
            // new SchemaExport(config).Drop(false, true);
            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(config).Create(false, true);
        }

        public static ISessionFactory CreateSessionFactory(string environment)
        {
            var connectString = GetConnectionString(environment);
            connectString = connectString.Replace("\\\\", "\\");

            return Fluently.Configure()
                .Database(
                    MsSqlConfiguration.MsSql2008.ConnectionString(connectString).ShowSql().AdoNetBatchSize(5)
                        .MaxFetchDepth(2))
                .Mappings(x => x.FluentMappings.AddFromAssemblyOf<Mappings.SubstanceMap>()
                                    .ExportTo(ExportPath))
                .CurrentSessionContext<NHibernate.Context.ThreadStaticSessionContext>()
                .Diagnostics(x =>
                                    {
                                        x.Enable(true);
                                        x.OutputToFile(LogPath);
                                    })
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private static string GetConnectionString(string environment)
        {
            return new DatabaseConnection().GetConnectionString(environment);
        }
    }
}
