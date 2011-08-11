using System;
using System.Threading;
using Informedica.GenForm.Assembler.Assemblers;
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
                            var instance = new GenFormApplication();
                            Thread.MemoryBarrier();
                            _instance = instance;
                            _factory = CreateSessionFactory();
                            Thread.MemoryBarrier();
                        }
                    }
                return _instance;
            }
        }

        public static ISessionFactory SessionFactory
        {
            get { return Instance.SessionFactoryFromInstance; }
        }

        [Obsolete]
        public ISessionFactory SessionFactoryFromInstance
        {
            get { return _factory; }
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
