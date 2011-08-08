using System;
using StructureMap;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Unit : Entity<Guid, UnitDto>, IUnit
    {
        private IUnitGroup _group;

        protected Unit(): base(new UnitDto()) {}

        [DefaultConstructor]
        public Unit(UnitDto dto): base(dto.CloneDto())
        {
            _group = new UnitGroup(new UnitGroupDto
                                       {
                                           Id = Dto.UnitGroupId,
                                           UnitGroupName = Dto.UnitGroupName,
                                           AllowConversion = Dto.AllowConversion
                                       });
        }

        #region Implementation of IUnit

        public virtual string Name
        {
            get { return Dto.Name; }
            set { Dto.Name = value; }
        }

        public virtual String Abbreviation
        {
            get { return Dto.Abbreviation; }
            set { Dto.Name = value; }
        }

        public virtual Decimal Multiplier
        {
            get { return Dto.Multiplier; }
            set { Dto.Multiplier = value; }
        }

        public virtual Boolean IsReference
        {
            get { return Dto.IsReference; }
            set { Dto.IsReference = value; }
        }

        public virtual IUnitGroup UnitGroup
        {
            get { return _group ?? (_group = new UnitGroup(new UnitGroupDto())); }
            set { _group = value; }
        }

        #endregion
    }

    public class UnitDto : DataTransferObject<UnitDto, Guid>
    {
        public override UnitDto CloneDto()
        {
            return CreateClone();
        }

        public Int32 UnitId;
        public String Name;
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
