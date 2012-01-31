namespace Informedica.GenForm.Presentation.Forms
{
    public class NumericField: FormField
    {
        private NumericField(FieldType type) : base(type)
        {
        }

        public static IFormField NewNumericField()
        {
            return new NumericField(FieldType.Numerical);
        }
    }
}
