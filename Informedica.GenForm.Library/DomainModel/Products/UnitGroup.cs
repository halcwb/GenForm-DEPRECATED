using System;
using System.Collections.Generic;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class UnitGroup : Entity<Guid, UnitGroupDto>, IUnitGroup
    {
        private IList<Unit> _units;

        protected UnitGroup(): base(new UnitGroupDto()){}

        public UnitGroup(UnitGroupDto dto): base(dto.CloneDto()) {}

        public virtual string UnitGroupName
        {
            get { return Dto.UnitGroupName; }
            set { Dto.UnitGroupName = value; }
        }

        public virtual bool AllowsConversion
        {
            get { return Dto.AllowConversion; }
            set { Dto.AllowConversion = value; }
        }

        public virtual IList<Unit> Units
        {
            get { return _units; }
            set { _units = value; }
        }
    }

    public class UnitGroupDto: DataTransferObject<UnitGroupDto, Guid>
    {
        public Int32 UnitGroupId;
        public String UnitGroupName;
        public Boolean AllowConversion;

        public override UnitGroupDto CloneDto()
        {
            return CreateClone();
        }

        #region Overrides of DataTransferObject<Guid>


        #endregion
    }
}