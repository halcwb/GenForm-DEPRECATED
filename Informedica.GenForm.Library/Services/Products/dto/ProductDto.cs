using System;
using System.Collections.Generic;

namespace Informedica.GenForm.Library.Services.Products.dto
{
    public class ProductDto: ICloneable
    {
        public int Id;
        public string ProductName;
        public string DisplayName;
        public string Generic;
        public string Brand;
        public string Shape;
        public string Package;
        public string Unit;
        public decimal Quantity;
        public IEnumerable<SubstanceDto> Substances = new List<SubstanceDto>();
        public IEnumerable<RouteDto> Routes = new List<RouteDto>();
        public string ProductCode;

        #region Implementation of ICloneable

        public object Clone()
        {
            return MemberwiseClone();
        }

        public ProductDto CloneDto()
        {
            return (ProductDto)Clone();
        }

        #endregion
    }
}