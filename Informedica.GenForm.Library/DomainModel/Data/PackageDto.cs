using System;
using System.Collections.Generic;

namespace Informedica.GenForm.Library.DomainModel.Data
{
    public class PackageDto: DataTransferObject<PackageDto>
    {
        public string Abbreviation;
        private IList<ShapeDto> _shapes;

        public IList<ShapeDto> Shapes { get { return _shapes ?? (_shapes = new List<ShapeDto>()); } set { _shapes = value; } }

        #region Overrides of DataTransferObject<PackageDto,Guid>

        public override PackageDto CloneDto()
        {
            return CreateClone();
        }

        #endregion
    }
}