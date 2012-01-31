using System.Collections.Generic;

namespace Informedica.GenForm.Library.DomainModel.Data
{
    public class ShapeDto: DataTransferObject<ShapeDto>
    {
        public ShapeDto()
        {
            Packages = new List<PackageDto>();
            UnitGroups = new List<UnitGroupDto>();
            Routes = new List<RouteDto>();
        }

        public IEnumerable<PackageDto> Packages { get; set; }

        public IEnumerable<UnitGroupDto> UnitGroups { get; set; }

        public IEnumerable<RouteDto> Routes { get; set; }

        #region Overrides of DataTransferObject<ShapeDto,Guid>

        public override ShapeDto CloneDto()
        {
            return CreateClone();
        }

        #endregion
    }
}