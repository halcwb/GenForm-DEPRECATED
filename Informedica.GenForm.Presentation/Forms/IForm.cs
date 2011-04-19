using System;
using System.Collections.Generic;

namespace Informedica.GenForm.Presentation.Forms
{
    public interface IForm
    {
        String Caption { get; set; }
        void AddField(IFormField field);
        void RemoveField(IFormField field);
        IList<IFormField> Fields { get; }
        void AddButton(IButton button);
        void RemoveButton(IButton button);
        IList<IButton> Buttons { get; }
    }
}
