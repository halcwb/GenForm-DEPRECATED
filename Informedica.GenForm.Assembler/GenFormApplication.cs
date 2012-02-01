using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        
        private static readonly IDictionary<string, ISessionFactory> Factories = new ConcurrentDictionary<string, ISessionFactory>();

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

        public static ISessionFactory TestSessionFactory { get { return SessionFactoryCreator.CreateSessionFactory(); } }

        private ISessionFactory SessionFactoryFromInstance
        {
            get
            {
                return GetSessionFactory("GenFormTest");
            } 
        }

        public static void Initialize()
        {
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry(DatabaseAssembler.RegisterDependencies());
                x.AddRegistry(DatabaseRegistrationAssembler.RegisterDependencies());
                x.AddRegistry(RepositoryAssembler.RegisterDependencies());
                x.AddRegistry(SettingsAssembler.RegisterDependencies());
            });
        }

        public static ISessionFactory GetSessionFactory(string environment)
        {
            if (!Factories.ContainsKey(environment))
            {
                Factories.Add(environment, SessionFactoryCreator.CreateSessionFactory(environment));
            }

            return Factories[environment];
        }
    }
}
