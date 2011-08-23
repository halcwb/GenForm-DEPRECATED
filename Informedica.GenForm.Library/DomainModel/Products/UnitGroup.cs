using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Relations;
using Informedica.GenForm.Library.Exceptions;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class UnitGroup : Entity<Guid, UnitGroupDto>, IUnitGroup, IRelationPart
    {
        private HashSet<Unit> _units;
        private HashSet<Shape> _shapes;

        protected UnitGroup(): base(new UnitGroupDto()){}

        private UnitGroup(UnitGroupDto dto): base(dto.CloneDto())
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
            get { return GetUnits(); }
            protected set { SetUnits(value); }
        }

        private void SetUnits(IEnumerable<Unit> value)
        {
            RelationManager.OneToMany<UnitGroup, Unit>().Add(this, new HashSet<Unit>(value));
        }

        private IEnumerable<Unit> GetUnits()
        {
            return new HashSet<Unit>(RelationManager.OneToMany<UnitGroup, Unit>().GetManyPart(this));
                //_units ?? (_units = new HashSet<Unit>(new UnitComparer()));
        }

        public virtual IEnumerable<Shape> Shapes
        {
            get { return GetShapes();  }
            protected set { _shapes = new HashSet<Shape>(value); }
        }

        public virtual void AddUnit(Unit unit)
        {
            RelationProvider.UnitGroupUnit.Add(this, unit);
        }

        public virtual void RemoveUnit(Unit unit)
        {
            RelationManager.OneToMany<UnitGroup, Unit>().Remove(this, unit);
            //GetUnits().RemoveWhere(x => new UnitComparer().Equals(x, unit));
        }

        private bool CannotAddUnit(Unit unit)
        {
            if (unit == null) return true;
            return ContainsUnit(unit);
        }

        public virtual bool ContainsUnit(Unit unit)
        {
            return GetUnits().Contains(unit, _units.Comparer);
        }

        public static bool Equals(UnitGroup x, UnitGroup y, UnitGroupComparer comparer)
        {
            return comparer.Equals(x, y);
        }

        public override bool IdIsDefault(Guid id)
        {
            return id == Guid.Empty;
        }

        public static UnitGroup Create(UnitGroupDto groupDto)
        {
            return new UnitGroup(groupDto);
        }

        public virtual void AddShape(Shape shape)
        {
            RelationManager.ManyToMany<Shape, UnitGroup>().Add(shape, this);
            //shape.AddUnitGroup(this, AddShapeToUnit);
        }

        private void AddShapeToUnit(Shape shape)
        {
            ShapeAssociation.AddShape(GetShapes(), shape);
        }

        private HashSet<Shape> GetShapes()
        {
            return new HashSet<Shape>(RelationManager.ManyToMany<Shape, UnitGroup>().GetManyPartLeft(this));
        }

        public virtual void RemoveShape(Shape shape)
        {
            RelationManager.ManyToMany<Shape, UnitGroup>().Remove(shape, this);
            //_shapes.Remove(shape);
            //shape.RemoveUnitGroup(this);
        }
    }
}