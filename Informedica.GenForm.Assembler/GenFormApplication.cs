using System;
using Informedica.GenForm.DataAccess;
using NHibernate;
using StructureMap;

namespace Informedica.GenForm.Assembler
{
    public class GenFormApplication
    {
        private static GenFormApplication _instance;
        private static readonly Object LockThis = new object();
        private static ISessionFactory _factory;

        private GenFormApplication() {}

        public static GenFormApplication Instance
        {
            get
            {
                if (_instance == null)
                    lock (LockThis)
                    {
                        if (_instance == null)
                        {
                            _instance = new GenFormApplication();
                        }
                    }
                return _instance;
            }
        }

        public ISessionFactory SessionFactory
        {
            get { return _factory ?? (_factory = CreateSessionFactory()); }
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return SessionFactoryCreator.CreateSessionFactory();            
        }

        public static void Initialize()
        {
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry(DatabaseAssembler.RegisterDependencies());
                x.AddRegistry(ProductAssembler.RegisterDependencies());
                x.AddRegistry(DatabaseRegistrationAssembler.RegisterDependencies());
                x.AddRegistry(UserAssembler.RegisterDependencies());
                x.AddRegistry(TransactionAssembler.RegisterDependencies());
                x.AddRegistry(RepositoryAssembler.RegisterDependencies());
            });
        }
    }
}
