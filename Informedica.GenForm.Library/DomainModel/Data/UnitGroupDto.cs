using System;

namespace Informedica.GenForm.Library.DomainModel.Data
{
    public class UnitGroupDto: DataTransferObject<UnitGroupDto, Guid>
    {
        public Boolean AllowConversion;

        public override UnitGroupDto CloneDto()
        {
            return CreateClone();
        }

    }
}