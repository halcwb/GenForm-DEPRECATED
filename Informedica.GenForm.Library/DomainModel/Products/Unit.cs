using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products.Data;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Unit : Entity<Guid, UnitDto>, IUnit
    {
        private UnitGroup _unitGroup;

        #region Constructor

        protected Unit() : base(new UnitDto()) { }

        private Unit(UnitDto dto) : base(dto.CloneDto())
        {
            var group = UnitGroup.Create(new UnitGroupDto
                {
                    AllowConversion = dto.AllowConversion,
                    Name = dto.UnitGroupName
                });
            SetUnitGroup(group);
        }

        public Unit(UnitDto dto, UnitGroup group)
            : base(dto.CloneDto())
        {
            SetUnitGroup(group);
        }

        private void SetUnitGroup(UnitGroup group)
        {
            ChangeUnitGroup(group);
        }

        #endregion

        #region Business

        public virtual String Abbreviation
        {
            get { return Dto.Abbreviation; }
            set { Dto.Abbreviation = value; }
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

        public virtual UnitGroup UnitGroup
        {
            get { return _unitGroup; }
            protected internal set { _unitGroup = value; }
        }

        public virtual void ChangeUnitGroup(UnitGroup newGroup)
        {
            newGroup.AddUnit(this);
        }

        public virtual bool CannotChangeGroup(UnitGroup newGroup)
        {
            return UnitGroup.Equals(newGroup, UnitGroup, new UnitGroupComparer());
        }

        public override bool IdIsDefault(Guid id)
        {
            return id == Guid.Empty;
        }

        internal protected virtual void RemoveFromGroup()
        {
            UnitGroup.RemoveUnit(this);
        }

        #endregion

        #region Factory

        public static Unit Create(UnitDto dto)
        {
            return new Unit(dto);
        }

        public static Unit Create(UnitDto dto, UnitGroup group)
        {
            return new Unit(dto, group);
        }

        public static Unit Create(UnitDto dto, UnitGroupDto groupDto)
        {
            return new Unit(dto, UnitGroup.Create(groupDto));
        }

        #endregion
    }
}
