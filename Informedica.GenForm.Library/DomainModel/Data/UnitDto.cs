using System;

namespace Informedica.GenForm.Library.DomainModel.Products.Data
{
    public class UnitDto : DataTransferObject<UnitDto, Guid>
    {
        public override UnitDto CloneDto()
        {
            return CreateClone();
        }

        public Int32 UnitId;
        public String Abbreviation;
        public Decimal Multiplier;
        public Decimal Divisor;
        public Boolean IsReference;
        public Guid UnitGroupId;
        public String UnitGroupName;
        public Boolean AllowConversion;

        #region Overrides of DataTransferObject<Guid>


        #endregion
    }
}