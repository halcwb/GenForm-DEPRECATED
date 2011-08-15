using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.Exceptions;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class UnitGroup : Entity<Guid, UnitGroupDto>, IUnitGroup
    {
        private HashSet<Unit> _units = new HashSet<Unit>(new UnitComparer());

        protected UnitGroup(): base(new UnitGroupDto()){}

        [Obsolete]
        public UnitGroup(UnitGroupDto dto): base(dto.CloneDto())
        {
            ValidateDto();
        }

        private void ValidateDto()
        {
            if (String.IsNullOrWhiteSpace(Dto.Name)) throw new InvalidDtoException<UnitGroupDto, Guid>(Dto);
        }

        public virtual bool AllowsConversion
        {
            get { return Dto.AllowConversion; }
            set { Dto.AllowConversion = value; }
        }

        public virtual IEnumerable<Unit> Units
        {
            get { return _units; }
            protected set { _units = new HashSet<Unit>(value); }
        }

        public virtual void AddUnit(Unit unit)
        {
            if (CannotAddUnit(unit)) throw new CannotAddItemException(unit);
            _units.Add(unit);
            unit.ChangeUnitGroup(this);
        }

        public virtual void RemoveUnit(Unit unit)
        {
            _units.RemoveWhere(x => new UnitComparer().Equals(x, unit));
        }

        private bool CannotAddUnit(Unit unit)
        {
            if (unit == null) return true;
            return ContainsUnit(unit);
        }

        public virtual bool ContainsUnit(Unit unit)
        {
            return _units.Contains(unit, _units.Comparer);
        }

        public static bool Equals(UnitGroup x, UnitGroup y, UnitGroupComparer comparer)
        {
            return comparer.Equals(x, y);
        }

        public override bool IdIsDefault(Guid id)
        {
            return id == Guid.Empty;
        }
    }
}