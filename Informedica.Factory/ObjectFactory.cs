using System;
using System.Threading;

namespace Informedica.Factory
{
    public class ObjectFactory
    {
        private static readonly object LockThis = new object();
        private static ObjectFactory _instance;

        private ObjectFactory() {}

        public static ObjectFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockThis)
                    {
                        var instance = new ObjectFactory();
                        Thread.MemoryBarrier();
                        if (_instance == null) _instance = instance;
                    }
                }
                return _instance;
            }
        }

        #region ConstructorOverloading

        public T Create<T>()
        {
            return Create<T>(new object[] { });
        }

        public T Create<T>(object arg1)
        {
            return Create<T>(new[] { arg1 });
        }

        public T Create<T>(object arg1, object arg2)
        {
            return Create<T>(new[] { arg1, arg2 });
        }

        public T Create<T>(object arg1, object arg2, object arg3)
        {
            return Create<T>(new[] { arg1, arg2, arg3 });
        }

        public T Create<T>(object[] args)
        {
            return (T)Activator.CreateInstance(GetRegisteredType<T>(), args);
        }

        public static Type GetRegisteredType<T>()
        {
            return StructureMap.ObjectFactory.Model.DefaultTypeFor(typeof(T));
        }

        #endregion

        #region StructureMapDelegation

        public T GetInstance<T>()
        {
            return StructureMap.ObjectFactory.GetInstance<T>();
        }

        public StructureMap.ExplicitArgsExpression With<T>(T arg)
        {
            return StructureMap.ObjectFactory.With(arg);
        }

        #endregion

    }
}
