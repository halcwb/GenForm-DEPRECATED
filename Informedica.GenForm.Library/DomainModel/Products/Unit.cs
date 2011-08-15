using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.Factories;
using StructureMap;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Unit : Entity<Guid, UnitDto>, IUnit
    {
        #region Private
        
        private UnitGroup _group;
        private HashSet<Shape> _shapes = new HashSet<Shape>(new ShapeComparer());

        #endregion

        #region Constructor

        protected Unit() : base(new UnitDto()) { }

        [DefaultConstructor, Obsolete]
        public Unit(UnitDto dto) : base(dto.CloneDto()) { }

        public Unit(UnitDto dto, UnitGroup group)
            : base(dto.CloneDto())
        {
            SetUnitGroup(group);
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
            get { return _group; }
            protected set { _group = value; }
        }

        public virtual void AddShape(Shape shape)
        {
            shape.AddUnit(this, AddShapeToUnit);
        }

        private void AddShapeToUnit(Shape shape)
        {
            ShapeAssociation.AddShape(_shapes, shape);
        }

        public virtual bool CanNotAddShape(Shape shape)
        {
            return (shape == null || _shapes.Contains(shape, _shapes.Comparer));
        }

        public virtual IEnumerable<Shape> Shapes
        {
            get { return _shapes; }
            protected set { _shapes = new HashSet<Shape>(value); }
        }

        public virtual void ChangeUnitGroup(UnitGroup newGroup)
        {
            SetUnitGroup(newGroup);
        }

        private void SetUnitGroup(UnitGroup newGroup)
        {
            if (CannotChangeGroup(newGroup)) return;
            var oldGroup = UnitGroup;
            if (oldGroup != null) oldGroup.RemoveUnit(this);
            UnitGroup = newGroup;
            if (!UnitGroup.ContainsUnit(this)) UnitGroup.AddUnit(this);
        }

        public virtual bool CannotChangeGroup(UnitGroup newGroup)
        {
            if (_group == null) return false;
            return UnitGroup.Equals(newGroup, UnitGroup, new UnitGroupComparer());
        }

        public override bool IdIsDefault(Guid id)
        {
            return id == Guid.Empty;
        }

        #endregion

        #region Factory

        public static Unit CreateUnit(UnitDto dto)
        {
            return UnitFactory.CreateUnit(dto);
        }

        public static Unit CreateUnit(UnitDto dto, UnitGroup group)
        {
            return UnitFactory.CreateUnit(dto, group);
        }

        public static Unit CreateUnit(UnitDto dto, UnitGroupDto groupDto)
        {
            return UnitFactory.CreateUnit(dto, groupDto);
        }

        #endregion
    }
}
