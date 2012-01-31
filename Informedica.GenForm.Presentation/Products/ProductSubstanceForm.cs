using Informedica.GenForm.Presentation.Forms;

namespace Informedica.GenForm.Presentation.Products
{
    public class ProductSubstanceForm : IProductSubstanceForm
    {
        private readonly IFormField _orderNumber = NumericField.NewNumericField();
        public IFormField OrderNumber { get { return _orderNumber; } }

        private readonly IComboBoxField _substance = ComboField.NewComboField();
        public IComboBoxField Substance
        {
            get { return  _substance; }
        }

        private readonly IFormField _quantity = NumericField.NewNumericField();
        public IFormField Quantity { get { return _quantity; } }

        private readonly IComboBoxField _unit = ComboField.NewComboField();
        public IComboBoxField Unit { get { return _unit; } }

        private readonly IFormField _concentration = NumericField.NewNumericField();
        public IFormField Concentration { get { return _concentration; } }

        private readonly IComboBoxField _numeratorUnit = ComboField.NewComboField();
        public IComboBoxField NumeratorUnit { get { return _numeratorUnit; } }

        private readonly IComboBoxField _denominatorUnit = ComboField.NewComboField();
        public IComboBoxField DenominatorUnit { get { return _denominatorUnit; } }

        private ProductSubstanceForm() {}

        public static IProductSubstanceForm NewDrugProductSubstanceForm()
        { return  new ProductSubstanceForm();}
    }
}
