using System;

namespace Informedica.GenForm.Library.DomainModel.Products.Data
{
    public class PackageDto: DataTransferObject<PackageDto, Guid>
    {
        public string Abbreviation;

        #region Overrides of DataTransferObject<PackageDto,Guid>

        public override PackageDto CloneDto()
        {
            return CreateClone();
        }

        #endregion
    }
}