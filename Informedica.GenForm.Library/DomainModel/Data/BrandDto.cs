using System;

namespace Informedica.GenForm.Library.DomainModel.Data
{
    public class BrandDto: DataTransferObject<BrandDto>
    {
        public override BrandDto CloneDto()
        {
            return CreateClone();
        }
    }
}