using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products.Collections;
using Informedica.GenForm.Library.DomainModel.Products.Interfaces;
using Informedica.GenForm.Library.DomainModel.Validation;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class UnitGroup : Entity<UnitGroup>, IUnitGroup
    {
        #region Private

        private UnitGroupDto _dto;
        private UnitCollection _units;
        private ShapeCollection<UnitGroup> _shapes;

        #endregion

        #region Construction

        static UnitGroup()
        {
            RegisterValidationRules();
        }

        protected UnitGroup()
        {
            _dto = new UnitGroupDto();
            InitCollections();
        }

        private UnitGroup(UnitGroupDto dto)
        {
            ValidateDto(dto);
            InitCollections();
        }

        private void InitCollections()
        {
            _units = new UnitCollection(this);
            _shapes = new ShapeCollection<UnitGroup>(this);
        }

        #endregion

        #region Business

        public override Guid Id { get { return _dto.Id; } protected set { _dto.Id = value; } }

        public override string Name { get { return _dto.Name; } protected set { _dto.Name = value; } }

        public virtual bool AllowsConversion
        {
            get { return _dto.AllowConversion; }
            set { _dto.AllowConversion = value; }
        }

        public virtual IEnumerable<IUnit> Units
        {
            get { return _units; }
        }

        public virtual Iesi.Collections.Generic.ISet<Unit> UnitSet
        {
            get { return _units.GetUnitSet(); }
            protected set { _units = new UnitCollection(value, this); }
        }

        public virtual void AddUnit(IUnit unit)
        {
            _units.Add((Unit)unit);
        }

        internal protected virtual void RemoveUnit(Unit unit)
        {
            _units.Remove(unit);
        }

        public virtual IEnumerable<IShape> Shapes
        {
            get { return _shapes; }
        }

        public virtual Iesi.Collections.Generic.ISet<Shape> ShapeSet
        {
            get { return _shapes.GetEntitySet(); }
            protected set { _shapes = new ShapeCollection<UnitGroup>(value, this); }
        }

        public virtual void AddShape(IShape shape)
        {
            _shapes.Add((Shape)shape, ((Shape)shape).AddUnitGroup);
        }

        public virtual void RemoveShape(IShape shape)
        {
            _shapes.Remove((Shape)shape, ((Shape)shape).RemoveUnitGroup);
        }

        public static bool Equals(UnitGroup x, UnitGroup y, UnitGroupComparer comparer)
        {
            return comparer.Equals(x, y);
        }

        #endregion

        #region Factory

        public static UnitGroup Create(UnitGroupDto groupDto)
        {
            return new UnitGroup(groupDto);
        }

        #endregion

        #region Validation

        private static void RegisterValidationRules()
        {
            ValidationRulesManager.RegisterRule<UnitGroupDto>(x => !String.IsNullOrWhiteSpace(x.Name));
        }

        protected override void SetDto<TDto>(TDto dto)
        {
            var dataTransferObject = dto as DataTransferObject<UnitGroupDto>;
            if (dataTransferObject != null) _dto = dataTransferObject.CloneDto();
        }

        #endregion
    }
}