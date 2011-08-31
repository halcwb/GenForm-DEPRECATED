using System;

namespace Informedica.GenForm.Library.DomainModel.Data
{
    public class UnitDto : DataTransferObject<UnitDto>
    {
        public override UnitDto CloneDto()
        {
            return CreateClone();
        }

        public String Abbreviation;
        public Decimal Multiplier;
        public Decimal Divisor;
        public Boolean IsReference;
        public Guid UnitGroupId;
        public String UnitGroupName;
        public Boolean AllowConversion;

    }
}