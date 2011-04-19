namespace Informedica.GenForm.ServiceProviders
{
    public class ConcreteServiceProvider: ServiceProvider
    {
        private static ConcreteServiceProvider _instance;
        private static readonly object LockThis = new object();

        private ConcreteServiceProvider() { }

        public static IServiceProvider Provider
        {
            get { return CreateInstance(_instance, LockThis); }
        }
    }
}
