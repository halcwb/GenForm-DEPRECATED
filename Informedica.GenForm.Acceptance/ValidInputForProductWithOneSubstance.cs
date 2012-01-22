namespace Informedica.GenForm.Acceptance
{
    public class ValidInputForProductWithOneSubstance
    {
        public string GenericName { get; set; }
        public string BrandName { get; set; }
        public string ShapeName { get; set; }
        public string ProductQuantity { get; set; }
        public string ProductUnit { get; set; }
        public string PackageName { get; set; }
        public string SubstanceOrder { get; set; }
        public string SubstanceName { get; set; }
        public string SubstanceQuantity { get; set; }
        public string SubstanceQuantityUnit { get; set; }
        public string SubstanceConcentration { get; set; }
        public string ConcentrationUnit { get; set; }

        public bool IsValid()
        {
            return false;
        }

        public string ErrorMessage { get; set; }
    }
}