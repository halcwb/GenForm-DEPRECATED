using System;
using System.Collections.Generic;

namespace Informedica.GenForm.Library.DomainModel.Products.Data
{
    public class RouteDto: DataTransferObject<RouteDto, Guid>
    {
        public RouteDto()
        {
            Shapes = new List<ShapeDto>();
        }

        public string Name;
        public string Abbreviation;

        public IEnumerable<ShapeDto> Shapes { get; set; }

        #region Overrides of DataTransferObject<RouteDto,Guid>

        public override RouteDto CloneDto()
        {
            return CreateClone();
        }

        #endregion
    }
}