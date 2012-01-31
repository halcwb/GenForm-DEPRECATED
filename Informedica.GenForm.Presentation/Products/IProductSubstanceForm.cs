using Informedica.GenForm.Presentation.Forms;

namespace Informedica.GenForm.Presentation.Products
{
    public interface IProductSubstanceForm
    {
        IFormField OrderNumber { get; }
        IComboBoxField Substance { get; }
        IFormField Quantity { get; }
        IComboBoxField Unit { get; }
        IFormField Concentration { get; }
        IComboBoxField NumeratorUnit { get; }
        IComboBoxField DenominatorUnit { get; }
    }
}