using System.Collections.Generic;

namespace Informedica.GenForm.Library.DomainModel.Products.Data
{
    public class ProductDto: DataTransferObject
    {
        public int Id;
        public string ProductName;
        public string DisplayName;
        public string GenericName;
        public string BrandName;
        public string ShapeName;
        public string PackageName;
        public string UnitName;
        public decimal Quantity;
        public IEnumerable<ProductSubstanceDto> Substances = new List<ProductSubstanceDto>();
        public IEnumerable<RouteDto> Routes = new List<RouteDto>();
        public string ProductCode;


        public ProductDto CloneDto()
        {
            return CloneDto<ProductDto>();
        }

    }
}