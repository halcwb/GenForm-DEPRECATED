using System.Collections.Generic;

namespace Informedica.GenForm.Library.DomainModel.Data
{
    public class ProductDto: DataTransferObject<ProductDto>
    {
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
        public string UnitAbbreviation;
        public bool UnitGroupAllowConversion;
        public decimal UnitDivisor;
        public bool UnitIsReference;
        public decimal UnitMultiplier;
        public string UnitGroupName;
        public string PackageAbbreviation;

        public override ProductDto CloneDto()
        {
            return CreateClone();
        }

    }
}