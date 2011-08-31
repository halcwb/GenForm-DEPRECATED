using System;
using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Validation;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class UnitGroup : Entity<UnitGroup>, IUnitGroup
    {
        #region Private

        private ISet<Unit> _units = new HashedSet<Unit>();
        private ISet<Shape> _shapes = new HashedSet<Shape>();
        private UnitGroupDto _dto;

        #endregion

        #region Construction

        static UnitGroup()
        {
            RegisterValidationRules();
        }

        protected UnitGroup() : base(new UnitGroupComparer())
        {
            _dto = new UnitGroupDto();
        }

        private UnitGroup(UnitGroupDto dto) : base(new UnitGroupComparer())
        {
            ValidateDto(dto);
        }

        #endregion

        #region Business

        public virtual bool AllowsConversion
        {
            get { return _dto.AllowConversion; }
            set { _dto.AllowConversion = value; }
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

        public override Guid Id { get { return _dto.Id; } protected set { _dto.Id = value; } }

        public override string Name { get { return _dto.Name; } protected set { _dto.Name = value; } }

        private static void RegisterValidationRules()
        {
            ValidationRulesManager.RegisterRule<UnitGroupDto>(x => !String.IsNullOrWhiteSpace(x.Name));
        }

        protected override void SetDto<TDto>(TDto dto)
        {
            var dataTransferObject = dto as DataTransferObject<UnitGroupDto>;
            if (dataTransferObject != null) _dto = dataTransferObject.CloneDto();
        }
    }
}