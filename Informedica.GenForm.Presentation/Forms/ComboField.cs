using System;
using System.Collections.Generic;
using System.Linq;

namespace Informedica.GenForm.Presentation.Forms
{
    public class ComboField: FormField, IComboBoxField
    {
        private ComboField(): base(FieldType.Combo) {}

        public static IComboBoxField NewComboField()
        {
            return new ComboField();
        }

        private IList<String> _values = new List<String>();
        public IList<String> Values
        {
            get
            {
                return _values.ToList();
            }
        }

        public void  AddValue(String value)
        {
            _values.Add(value);
        }

        public  void  AddRange(IList<String> list)
        {
            _values = _values.Concat(list).ToList();
        }
    }
}
