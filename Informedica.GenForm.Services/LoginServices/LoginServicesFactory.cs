using Informedica.GenForm.Library.Services;

namespace Informedica.GenForm.ServiceProviders.LoginServices
{
    class LoginServicesFactory
    {
        public static ILoginServices CreateLoginServices()
        {
            return Library.Services.LoginServices.NewLoginServices();
        }
    }
}
