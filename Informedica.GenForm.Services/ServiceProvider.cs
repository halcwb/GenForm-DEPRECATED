using System;
using Microsoft.Practices.Unity;

namespace Informedica.GenForm.ServiceProviders
{
    public abstract class ServiceProvider : IServiceProvider
    {
        private readonly IUnityContainer _container;

        protected ServiceProvider()
        { _container = new UnityContainer(); }

        #region IServiceProvider Members

        protected void RegisterInstance<T>(T instance)
        {
            _container.RegisterInstance(instance);
        }

        protected static T CreateInstance<T>(T instance, object lockThis) where T : class, IServiceProvider
        {
            if (instance == null)
            {
                lock (lockThis)
                {
                    // ReSharper disable ConditionIsAlwaysTrueOrFalse
                    if (instance == null)
                    // ReSharper restore ConditionIsAlwaysTrueOrFalse
                    {
                        instance = (T)Activator.CreateInstance(typeof(T), true);
                    }
                }
            }
            return instance;
        }

        public T Resolve<T>()
        {
            try
            {
                return _container.Resolve<T>();

            }
            catch (Exception)
            {
                return default(T);
            }
        }

        #endregion

        #region IServiceProvider Members

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        #endregion

        public void RegisterInstanceOfType<T>(T instance)
        {
            RegisterInstance(instance);
        }
    }
}
