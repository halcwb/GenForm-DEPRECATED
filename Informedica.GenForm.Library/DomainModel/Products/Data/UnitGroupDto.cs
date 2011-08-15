using System;

namespace Informedica.GenForm.Library.DomainModel.Products.Data
{
    public class UnitGroupDto: DataTransferObject<UnitGroupDto, Guid>
    {
        public Int32 UnitGroupId;
        public Boolean AllowConversion;

        public override UnitGroupDto CloneDto()
        {
            return CreateClone();
        }

    }
}