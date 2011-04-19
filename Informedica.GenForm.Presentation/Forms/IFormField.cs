using System;

namespace Informedica.GenForm.Presentation.Forms
{
    /// <summary>
    /// Describes the visual presentation of a Formfield.
    /// </summary>
    public interface IFormField
    {
        String FieldName { get; set; }
        FieldType Type { get; }
        String Value { get; set; }
        Int32 MaxLength { get; set; }
        Int32 MinLength { get; set; }
        Boolean Required { get; set; }
        Boolean IsUnique { get; set; }
        String ErrorMessage { get; set; }
        String InformationMessage { get; set; }
        Boolean IsVisible { get; set; }
    }
}
