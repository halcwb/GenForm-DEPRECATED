namespace Informedica.GenForm.ServiceProviders
{
    public interface IServiceProvider: System.IServiceProvider
    {
        T Resolve<T>();
        void RegisterInstanceOfType<T>(T instance);
    }
}
