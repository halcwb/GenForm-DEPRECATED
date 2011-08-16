using System;

namespace Informedica.GenForm.Library.DomainModel.Data
{
    public class BrandDto: DataTransferObject<BrandDto, Guid>
    {
        public override BrandDto CloneDto()
        {
            return CreateClone();
        }
    }
}