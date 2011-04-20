using Informedica.GenForm.ServiceProviders;
using IServiceProvider = Informedica.GenForm.ServiceProviders.IServiceProvider;

namespace Informedica.GenForm.Library.ServiceProviders
{
    public class DalServiceProvider: ServiceProvider
    {
#pragma warning disable 649
        private static DalServiceProvider _instance;
#pragma warning restore 649
        private static readonly object LockThis = new object();

        private DalServiceProvider() { }

        public static IServiceProvider Instance
        {
            get { return GetSingletonInGetInstance(); }
        }

        private static IServiceProvider GetSingletonInGetInstance()
        {
            return _instance ?? (_instance = CreateInstance(_instance, LockThis));
        }
    }
}
