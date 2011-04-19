using Informedica.GenForm.PresentationLayer.Products;
using Informedica.GenForm.PresentationLayer.Security;
using Informedica.GenForm.ServiceProviders;

namespace Informedica.GenForm.Presentation.Services
{
    public class PresentationProvider: ServiceProvider
    {
        private static PresentationProvider _instance;
        private static readonly object LockThis = new object();

        private PresentationProvider() { RegisterInstances();  }

        public static IServiceProvider Provider
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockThis)
                    {
                        if (_instance == null)
                        {
                            _instance = new PresentationProvider();
                        }
                    }
                }
                return _instance;
            }
        }

        private void RegisterInstances()
        {
            RegisterInstance(GetProductForm());
        }

        private static IProductForm GetProductForm()
        {
            return ProductForm.NewDrugProductForm(); 
        }

    }
}
