using System;

namespace Informedica.GenForm.Presentation.Forms
{
    public class Button : IButton
    {
        #region Implementation of IButton

        public string Caption { get; private set; }

        public bool Enabled { get; set; }

        public bool IsPressed { get; set; }

        #endregion

        private Button(String caption)
        {
            Caption = caption ?? String.Empty;
        }

        public static IButton NewButton()
        {
            return NewButton(null);
        }

        public static IButton NewButton(String caption)
        {
            return new Button(caption) { Enabled = true };
        }
    }
}