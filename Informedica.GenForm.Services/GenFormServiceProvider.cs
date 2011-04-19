using System;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repository;
using Informedica.GenForm.ServiceProviders;
using IServiceProvider = Informedica.GenForm.ServiceProviders.IServiceProvider;

namespace Informedica.GenForm.Library.Services
{
    public class GenFormServiceProvider : ServiceProvider
    {
#pragma warning disable 649
        private static GenFormServiceProvider _instance;
#pragma warning restore 649
        private static readonly object LockThis = new object();

        private GenFormServiceProvider() { Initialize(); }

        public static IServiceProvider Instance
        {
            get { return GetSingletonInGetInstance(); }
        }

        private static IServiceProvider GetSingletonInGetInstance()
        {
            return _instance ?? (_instance = CreateInstance(_instance, LockThis));
        }

        private void Initialize()
        {
            RegisterInstance(GetLoginServices());
            RegisterInstance(GetProductService());
        }

        static ILoginServices GetLoginServices()
        {
            return new LoginServices();
        }

        static IProductServices GetProductService()
        {
            return new ProductServices();
        }

    }
}
