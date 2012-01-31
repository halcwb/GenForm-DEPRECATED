using System.Collections.ObjectModel;
using Informedica.GenForm.Presentation.Forms;

namespace Informedica.GenForm.Presentation.Products
{
    /// <summary>
    /// Describes the visual presentation of a input
    /// form to create or update a DrugProduct. Used to
    /// inform the GUI how to present the DrugProduct Form.
    /// </summary>
    public interface IProductForm
    {
        IFormField Id { get; }
        IFormField DisplayName { get; }
        IFormField DefaultName { get; }
        IComboBoxField Brand { get; }
        IFormField Divisor { get; }
        IComboBoxField Generic { get; }
        IComboBoxField Shape { get; }
        IComboBoxField Package { get; }
        IFormField Quantity { get; }
        IComboBoxField Unit { get; }
        IFormField TradeCode { get; }
        IFormField ProductCode { get; }
        IFormField Version { get; }
        ReadOnlyCollection<IProductSubstanceForm> Substances { get; }
        IButton CancelButton { get; }
        IButton SaveButton { get; }
        void AddSubstanceForm(IProductSubstanceForm substanceForm);
    }
}
