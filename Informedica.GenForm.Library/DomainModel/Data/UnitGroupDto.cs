using System;

namespace Informedica.GenForm.Library.DomainModel.Data
{
    public class UnitGroupDto: DataTransferObject<UnitGroupDto>
    {
        public Boolean AllowConversion;

        public override UnitGroupDto CloneDto()
        {
            return CreateClone();
        }

    }
}