using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Products.Data;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Product : Entity<Guid, ProductDto>, IProduct
    {
        private IList<ProductSubstance> _substances;

        protected Product(): base( new ProductDto()) {}

        public Product(ProductDto dto): base(dto.CloneDto())
        {
            foreach (var substanceDto in Dto.Substances)
            {
                GetSubstances().Add(NewSubstance(substanceDto));
            }
        }

        private static ProductSubstance NewSubstance(ProductSubstanceDto productSubstanceDto)
        {
            return new ProductSubstance(productSubstanceDto);
        }

        #region Implementation of IProduct

        public virtual string ProductCode { get { return Dto.ProductCode; } protected set { Dto.ProductCode = value; } }

        public virtual string GenericName { get { return Dto.GenericName; } protected set { Dto.GenericName = value; } }

        public virtual UnitValue Quantity { get; protected set; }

        public virtual string DisplayName { get { return Dto.DisplayName ?? Dto.ProductName; } protected set { Dto.DisplayName = value; } }

        public virtual ProductSubstance AddSubstance(ProductSubstanceDto productSubstanceDto)
        {
            ProductSubstance substance = new ProductSubstance(productSubstanceDto);
            GetSubstances().Add(substance);
            return substance;
        }

        private IList<ProductSubstance> GetSubstances()
        {
            return _substances ?? (_substances = new List<ProductSubstance>());
        }

        public virtual IEnumerable<ProductSubstance> Substances
        {
            get 
            {
                return GetSubstances();
            }
        }

        public virtual Brand Brand { get; protected set; }

        public virtual Package Package { get; protected set; }

        public virtual Shape Shape { get; protected set; }

        #endregion

        public override bool IdIsDefault(Guid id)
        {
            return id == Guid.Empty;
        }
    }
}