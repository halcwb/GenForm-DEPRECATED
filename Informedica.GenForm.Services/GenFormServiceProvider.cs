using Informedica.GenForm.Library.Security;
using Informedica.GenForm.Library.Services;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.ServiceProviders
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
            RegisterInstance(GetLoginUser());
        }

        static ILoginUser GetLoginUser()
        {
            return Isolate.Fake.Instance<ILoginUser>();
        }

        static ILoginServices GetLoginServices()
        {
            return Isolate.Fake.Instance<ILoginServices>();
        }

        static IProductServices GetProductService()
        {
            return Isolate.Fake.Instance<IProductServices>();
        }

    }
}
