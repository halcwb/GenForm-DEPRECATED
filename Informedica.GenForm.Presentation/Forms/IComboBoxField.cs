using System;
using System.Collections.Generic;

namespace Informedica.GenForm.Presentation.Forms
{
    public interface IComboBoxField: IFormField
    {
        IList<String> Values { get; }
        void AddValue(String value);
        void AddRange(IList<String> list);
    }

}
