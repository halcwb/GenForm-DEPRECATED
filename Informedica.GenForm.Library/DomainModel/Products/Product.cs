using System.Collections.Generic;
using Informedica.GenForm.Library.Services.Products.dto;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Product : IProduct
    {
        private IList<IProductSubstance> _substances;
        private ProductDto _dto;

        public Product() { _dto = new ProductDto(); }

        public Product(ProductDto dto)
        {
            _dto = dto.CloneDto();
            if (dto.Substances == null) return;
            
            foreach (var substanceDto in dto.Substances)
            {
                GetSubstances().Add(NewSubstance(substanceDto));
            }
        }

        private static IProductSubstance NewSubstance(SubstanceDto substanceDto)
        {
            return new ProductSubstance(substanceDto);
        }

        #region Implementation of IProduct

        public int ProductId { get { return _dto.Id; } set { _dto.Id = value; } }
        public string ProductName { get { return _dto.ProductName; } set { _dto.ProductName = value; } }
        public string ProductCode { get { return _dto.ProductCode; } set { _dto.ProductCode = value; } }
        public string GenericName { get { return _dto.Generic; } set { _dto.Generic = value; } }
        public string BrandName { get { return _dto.Brand; } set { _dto.Brand = value; } }
        public string ShapeName { get { return _dto.Shape; } set { _dto.Shape = value; } }
        public decimal Quantity { get { return _dto.Quantity; } set { _dto.Quantity = value; } }
        public string UnitName { get { return _dto.Unit; } set { _dto.Unit = value; } }
        public string PackageName { get { return _dto.Package; } set { _dto.Package = value; } }
        public string DisplayName { get { return _dto.DisplayName; } set { _dto.DisplayName = value; } }

        public IProductSubstance AddSubstance(SubstanceDto substanceDto)
        {
            IProductSubstance substance = new ProductSubstance(substanceDto);
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