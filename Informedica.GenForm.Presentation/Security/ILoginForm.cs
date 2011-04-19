using Informedica.GenForm.Presentation.Forms;

namespace Informedica.GenForm.PresentationLayer.Security
{
    public interface ILoginForm
    {
        IFormField UserName { get;  }
        IFormField Password { get; }
        IButton Login { get;  }
    }
}
