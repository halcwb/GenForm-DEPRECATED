using System;
using Informedica.GenForm.Library.DomainModel.Products.Data;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class ProductSubstance : IProductSubstance
    {
        private readonly ProductSubstanceDto _dto;

        protected ProductSubstance() { _dto = new ProductSubstanceDto(); }

        public ProductSubstance(ProductSubstanceDto productSubstanceDto)
        {
            _dto = productSubstanceDto;
        }

        #region Implementation of IProductSubstance

        public virtual Int32 Id { get; protected set; }
        public virtual int SortOrder { get { return _dto.SortOrder; } set { _dto.SortOrder = value; } }
        public virtual Substance Substance { get; protected set; }
        public virtual UnitValue Quantity { get; protected set; }

        public virtual Product Product { get; protected set; }

        #endregion
    }
}