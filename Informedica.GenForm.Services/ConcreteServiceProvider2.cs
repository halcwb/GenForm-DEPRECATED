namespace Informedica.GenForm.ServiceProviders
{
    public class ConcreteServiceProvider2: ServiceProvider
    {
        private static ConcreteServiceProvider2 _instance;
        private static object _lockThis = new object();

        private ConcreteServiceProvider2() { }

        public static IServiceProvider Provider
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockThis)
                    {
                        if (_instance == null)
                        {
                            _instance = new ConcreteServiceProvider2();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
