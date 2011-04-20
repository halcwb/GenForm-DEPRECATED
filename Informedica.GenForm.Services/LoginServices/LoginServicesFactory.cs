using Informedica.GenForm.Library.Services;

namespace Informedica.GenForm.ServiceProviders.LoginServices
{
    class LoginServicesFactory
    {
        public static ILoginServices CreateLoginServices()
        {
            return new Library.Services.LoginServices();
        }
    }
}
