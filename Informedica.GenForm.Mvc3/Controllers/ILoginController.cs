using Informedica.GenForm.Services;

namespace Informedica.GenForm.Mvc3.Controllers
{
    public interface ILoginController
    {
        IDatabaseServices DatabaseServices { get; }
    }
}