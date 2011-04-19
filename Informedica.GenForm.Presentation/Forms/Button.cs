using System;

namespace Informedica.GenForm.Presentation.Forms
{
    public class Button : IButton
    {
        #region Implementation of IButton

        private String _caption;

        private Boolean _enabled;

        private Boolean _isPressed;

        public String Caption
        {
            get { return _caption; }
        }

        public Boolean Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public Boolean IsPressed
        {
            get { return _isPressed; }
            set { _isPressed = value; }
        }

        #endregion

        private Button(String caption)
        {
            _caption = caption ?? String.Empty;
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