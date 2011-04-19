using System.Collections.ObjectModel;
using System.Collections.Generic;
using Informedica.GenForm.Presentation.Forms;

namespace Informedica.GenForm.PresentationLayer.Products
{
    public class ProductForm : IProductForm
    {
        #region Implementation of IDrugProductForm

        private readonly IFormField _id = TextField.NewTextField();

        private readonly IFormField _displayName = TextField.NewTextField();

        private readonly IFormField _defaultName = TextField.NewTextField();

        private readonly IComboBoxField _brand = ComboField.NewComboField();

        private readonly IFormField _divisor = NumericField.NewNumericField();

        private readonly IComboBoxField _generic = ComboField.NewComboField();

        private readonly IComboBoxField _shape = ComboField.NewComboField();

        private readonly IComboBoxField _package = ComboField.NewComboField();

        private readonly IFormField _quantity = NumericField.NewNumericField();

        private readonly IComboBoxField _unit = ComboField.NewComboField();

        private readonly IFormField _tradeCode = TextField.NewTextField();

        private readonly IFormField _productCode = TextField.NewTextField();

        private readonly IFormField _version = VersionField.NewVersionField();

        private readonly IList<IProductSubstanceForm> _substances = new List<IProductSubstanceForm>();

        private readonly IButton _saveButton = Button.NewButton("Opslaan");

        private readonly IButton _cancelButton = Button.NewButton("Stoppen");

        public IFormField Id
        {
            get { return _id; }
        }

        public IFormField DisplayName
        {
            get { return _displayName; }
        }

        public IFormField DefaultName
        {
            get { return _defaultName; }
        }

        public IComboBoxField Brand
        {
            get { return _brand; }
        }

        public IFormField Divisor
        {
            get { return _divisor; }
        }

        public IComboBoxField Generic
        {
            get { return _generic; }
        }

        public IComboBoxField Shape
        {
            get { return _shape; }
        }

        public IComboBoxField Package
        {
            get { return _package; }
        }

        public IFormField Quantity
        {
            get { return _quantity; }
        }

        public IComboBoxField Unit
        {
            get { return _unit; }
        }

        public IFormField TradeCode
        {
            get { return _tradeCode; }
        }

        public IFormField ProductCode
        {
            get { return _productCode; }
        }

        public IFormField Version
        {
            get { return _version; }
        }

        public ReadOnlyCollection<IProductSubstanceForm> Substances
        {
            get
            {
                return new ReadOnlyCollection<IProductSubstanceForm>(_substances);
            }
        }

        public void AddSubstanceForm(IProductSubstanceForm substanceForm)
        {
            _substances.Add(substanceForm);
        }

        public IButton CancelButton
        {
            get { return _cancelButton; }
        }

        public  IButton SaveButton
        {
            get { return _saveButton; }
        }

        #endregion

        private ProductForm() { }

        public static IProductForm NewDrugProductForm()
        {
            var form = new ProductForm();
            form.AddSubstanceForm(ProductSubstanceForm.NewDrugProductSubstanceForm());
            form._saveButton.Enabled = false;
            form._cancelButton.Enabled = false;
            return form;
        }
    }
}