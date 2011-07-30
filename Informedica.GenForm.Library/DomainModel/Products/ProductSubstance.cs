namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class ProductSubstance : IProductSubstance
    {
        #region Implementation of IProductSubstance

        public int SortOrder { get; set; }
        public string Substance { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }

        #endregion
    }
}