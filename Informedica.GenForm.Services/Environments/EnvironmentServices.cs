using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Settings.Environments;
using NHibernate;
using StructureMap;

namespace Informedica.GenForm.Services.Environments
{
    public class EnvironmentServices
    {
        public static IEnumerable<GenFormEnvironment> GetEnvironments(string machine)
        {
            var list = GenFormEnvironmentCollection.Create();
            if (!list.Any(e => e.MachineName == System.Environment.MachineName && e.Name == "TestGenForm"))
            {
                list.Add(GenFormEnvironment.CreateTest());
            }
            return list.Where(e => e.MachineName == machine);
        }

        public static IEnumerable<GenFormEnvironment> GetEnvironments()
        {
            return GetEnvironments(System.Environment.MachineName);
        }

        public static bool AddNewEnvironment(EnvironmentDto dto)
        {
            var list = GenFormEnvironmentCollection.Create();
            try
            {
                list.Add(GenFormEnvironment.Create(dto.Name, dto.provider, dto.Database, dto.LogPath, dto.ExportPath));
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public static void SetEnvironment(string environment)
        {
            if (HttpContext.Current == null) throw  new NullReferenceException("No HttpContext!");
            ObjectFactory.Configure(x => x.For<ISessionFactory>().HybridHttpOrThreadLocalScoped().Use(GenFormApplication.GetSessionFactory(environment)));
        }
    }
}