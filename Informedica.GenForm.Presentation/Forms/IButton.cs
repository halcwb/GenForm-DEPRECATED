using System;

namespace Informedica.GenForm.Presentation.Forms
{
    /// <summary>
    /// Describes the visual presentation of a Button.
    /// </summary>
    public interface IButton
    {
        String Caption { get; }
        Boolean Enabled { get; set; }
        Boolean IsPressed { get; set; }
    }
}
