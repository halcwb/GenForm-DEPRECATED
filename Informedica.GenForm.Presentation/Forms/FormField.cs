using System;

namespace Informedica.GenForm.Presentation.Forms
{
    public abstract class FormField: IFormField
    {
        private static FieldType _type;


        protected FormField(FieldType type)
        {
            _type = type;
        }

        private String _fieldName;
        public virtual String FieldName
        {
            get { return _fieldName ?? String.Empty; }
            set { _fieldName = value; }
        }

        public virtual FieldType Type
        {
            get { return  _type; }
        }

        private String _value;
        public virtual String Value
        {
            get { return _value ?? String.Empty; }
            set { _value = value; }
        }

        private Int32 _maxLength;
        public virtual Int32 MaxLength
        {
            get { return  _maxLength == 0 ? Int32.MaxValue: _maxLength; }
            set { _maxLength = value; }
        }

        public virtual int MinLength { get; set; }

        public virtual bool Required { get; set; }

        public virtual bool IsUnique { get; set; }

        private String _errorMesssage;
        public virtual String ErrorMessage
        {
            get { return  _errorMesssage ?? String.Empty; }
            set { _errorMesssage = value; }
        }

        private String _infoMessage;
        public virtual String InformationMessage
        {
            get { return _infoMessage ?? String.Empty; }
            set { _infoMessage = value; }
        }

        private Boolean _isVisible = true;
        public bool IsVisible
        {
            get { return  _isVisible; }
            set { _isVisible = value; }
        }
    }
}
