using System;
using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.Exceptions;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class UnitGroup : Entity<Guid, UnitGroupDto>, IUnitGroup
    {
        #region Private

        private ISet<Unit> _units = new HashedSet<Unit>();
        private ISet<Shape> _shapes = new HashedSet<Shape>();

        #endregion

        #region Construction

        protected UnitGroup() : base(new UnitGroupDto()) { }

        private UnitGroup(UnitGroupDto dto) : base(dto.CloneDto())
        {
            ValidateDto();
        }

        private void ValidateDto()
        {
            if (String.IsNullOrWhiteSpace(Dto.Name)) throw new InvalidDtoException<UnitGroupDto, Guid>(Dto);
        }

        #endregion

        #region Business

        public virtual bool AllowsConversion
        {
            get { return Dto.AllowConversion; }
            set { Dto.AllowConversion = value; }
        }

        public virtual ISet<Unit> Units
        {
            get { return _units; }
            protected set { _units = value; }
        }

        public virtual ISet<Shape> Shapes
        {
            get { return _shapes; }
            protected set { _shapes = value; }
        }

        public virtual void AddUnit(Unit unit)
        {
            if (_units.Contains(unit)) return;

            _units.Add(unit);
            unit.UnitGroup = this;
        }

        public virtual void RemoveUnit(Unit unit)
        {
            if (_units.Contains(unit))
            {
                _units.Remove(unit);
            }
        }

        public static bool Equals(UnitGroup x, UnitGroup y, UnitGroupComparer comparer)
        {
            return comparer.Equals(x, y);
        }

        public override bool IdIsDefault(Guid id)
        {
            return id == Guid.Empty;
        }

        public virtual void AddShape(Shape shape)
        {
            if (_shapes.Contains(shape)) return;

            _shapes.Add(shape);
            shape.AddUnitGroup(this);
        }

        public virtual void RemoveShape(Shape shape)
        {
            if (_shapes.Contains(shape))
            {
                _shapes.Remove(shape);
                shape.RemoveUnitGroup(this);
            }
        }

        #endregion

        #region Factory

        public static UnitGroup Create(UnitGroupDto groupDto)
        {
            return new UnitGroup(groupDto);
        }


        #endregion

    }
}