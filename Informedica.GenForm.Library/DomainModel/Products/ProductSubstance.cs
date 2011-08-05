using Informedica.GenForm.Library.DomainModel.Products.Data;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class ProductSubstance : IProductSubstance
    {
        private readonly ProductSubstanceDto _dto;

        public ProductSubstance(ProductSubstanceDto productSubstanceDto)
        {
            _dto = productSubstanceDto;
        }

        #region Implementation of IProductSubstance

        public int SortOrder { get { return _dto.SortOrder; } set { _dto.SortOrder = value; } }
        public string Substance { get { return _dto.Substance; } set { _dto.Substance = value; } }
        public decimal Quantity { get { return _dto.Quantity; } set { _dto.Quantity = value; } }
        public string Unit { get { return _dto.Unit; } set { _dto.Unit = value; } }

        #endregion
    }
}