using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Products.Data;

namespace Informedica.GenForm.Library.DomainModel.Data
{
    public class RouteDto: DataTransferObject<RouteDto, Guid>
    {
        public RouteDto()
        {
            Shapes = new List<ShapeDto>();
        }

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