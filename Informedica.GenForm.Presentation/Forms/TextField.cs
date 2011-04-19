using Informedica.GenForm.Presentation.Forms;

namespace Informedica.GenForm.Presentation.Forms
{
    public class TextField: FormField
    {
        private TextField() : base(FieldType.Text) {}

        public static IFormField NewTextField()
        {
            return new TextField();
        }

    }
}
