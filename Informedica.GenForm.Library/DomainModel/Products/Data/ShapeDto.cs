using System;
using System.Collections.Generic;

namespace Informedica.GenForm.Library.DomainModel.Products.Data
{
    public class ShapeDto: DataTransferObject<ShapeDto, Guid>
    {
        public ShapeDto()
        {
            Packages = new List<PackageDto>();
            Units = new List<UnitDto>();
            Routes = new List<RouteDto>();
        }

        public IEnumerable<PackageDto> Packages { get; set; }

        public IEnumerable<UnitDto> Units { get; set; }

        public IEnumerable<RouteDto> Routes { get; set; }

        #region Overrides of DataTransferObject<ShapeDto,Guid>

        public override ShapeDto CloneDto()
        {
            return CreateClone();
        }

        #endregion
    }
}