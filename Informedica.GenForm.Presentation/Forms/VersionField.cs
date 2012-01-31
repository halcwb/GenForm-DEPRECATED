namespace Informedica.GenForm.Presentation.Forms
{
    public class VersionField: FormField
    {
        private VersionField(FieldType type) : base(type)
        {
        }

        public  static IFormField NewVersionField()
        {
            var field =  new VersionField(FieldType.Version) {IsVisible = false};
            return field;
        }
    }
}
