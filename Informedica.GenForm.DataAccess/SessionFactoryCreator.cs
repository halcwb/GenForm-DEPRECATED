using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Informedica.GenForm.DataAccess.Databases;
using Informedica.Settings;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using StructureMap;

namespace Informedica.GenForm.DataAccess
{
    public static class SessionFactoryCreator
    {
        private static readonly string ExportPath = SettingsManager.Instance.GetExporthPath();
        private static readonly string LogPath = SettingsManager.Instance.GetLogPath();
        private static Configuration _configuration;

        static SessionFactoryCreator()
        {
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
        }

        public static ISessionFactory CreateSessionFactory()
        {
            return CreateSessionFactory("GenFormTest");
        }

        public static void BuildSchema(ISession session)
        {
            // first drop the database to recreate a new one
            // new SchemaExport(config).Drop(false, true);
            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(_configuration).Execute(false, true, false, session.Connection, null);
        }

        public static ISessionFactory CreateSessionFactory(string environment)
        {
            var connectString = GetConnectionString(environment);
            connectString = connectString.Replace("\\\\", "\\");

            return GetFluentConfiguration(connectString).BuildSessionFactory();
        }


        private static FluentConfiguration GetFluentConfiguration(string connectString)
        {
            return Fluently.Configure()
                .Database(GetDatabase(connectString))
                .Mappings(x => x.FluentMappings.AddFromAssemblyOf<Mappings.SubstanceMap>()
                                   .ExportTo(ExportPath))
                .CurrentSessionContext<NHibernate.Context.ThreadStaticSessionContext>()
                .Diagnostics(x =>
                                 {
                                     x.Enable(true);
                                     x.OutputToFile(LogPath);
                                 })
                .ExposeConfiguration(cfg => _configuration = cfg);
        }

        private static IPersistenceConfigurer GetDatabase(string connectString)
        {
            var config = ObjectFactory.GetInstance<IDatabaseConfig>();
            return config.Configurer(connectString);
        }

        private static string GetConnectionString(string environment)
        {
            return new DatabaseConnection().GetConnectionString(environment);
        }
    }
}
