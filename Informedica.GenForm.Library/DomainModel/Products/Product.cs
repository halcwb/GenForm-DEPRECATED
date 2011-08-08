using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Products.Data;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Product : Entity<Guid, ProductDto>, IProduct
    {
        private IList<IProductSubstance> _substances;

        public Product(): base( new ProductDto()) {}

        public Product(ProductDto dto): base(dto.CloneDto())
        {
            foreach (var substanceDto in Dto.Substances)
            {
                GetSubstances().Add(NewSubstance(substanceDto));
            }
        }

        private static IProductSubstance NewSubstance(ProductSubstanceDto productSubstanceDto)
        {
            return new ProductSubstance(productSubstanceDto);
        }

        #region Implementation of IProduct

        public int ProductProductId { get { return Dto.ProductId; } set { Dto.ProductId = value; } }

        public int ProductId
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string ProductName { get { return Dto.ProductName; } set { Dto.ProductName = value; } }
        public string ProductCode { get { return Dto.ProductCode; } set { Dto.ProductCode = value; } }
        public string GenericName { get { return Dto.GenericName; } set { Dto.GenericName = value; } }
        public string BrandName { get { return Dto.BrandName; } set { Dto.BrandName = value; } }
        public string ShapeName { get { return Dto.ShapeName; } set { Dto.ShapeName = value; } }
        public decimal Quantity { get { return Dto.Quantity; } set { Dto.Quantity = value; } }
        public string UnitName { get { return Dto.UnitName; } set { Dto.UnitName = value; } }
        public string PackageName { get { return Dto.PackageName; } set { Dto.PackageName = value; } }
        public string DisplayName { get { return Dto.DisplayName ?? Dto.ProductName; } set { Dto.DisplayName = value; } }

        public IProductSubstance AddSubstance(ProductSubstanceDto productSubstanceDto)
        {
            IProductSubstance substance = new ProductSubstance(productSubstanceDto);
            GetSubstances().Add(substance);
            return substance;
        }

        private IList<IProductSubstance> GetSubstances()
        {
            return _substances ?? (_substances = new List<IProductSubstance>());
        }

        public IEnumerable<IProductSubstance> Substances
        {
            get 
            {
                return GetSubstances();
            }
        }

        #endregion

    }
}