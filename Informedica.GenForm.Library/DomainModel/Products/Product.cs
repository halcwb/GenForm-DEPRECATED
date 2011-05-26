namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Product : IProduct
    {
        #region Implementation of IProduct

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string GenericName { get; set; }
        public string BrandName { get; set; }
        public string ShapeName { get; set; }
        public double Quantity { get; set; }
        public string UnitName { get; set; }
        public string PackageName { get; set; }

        #endregion
    }
}