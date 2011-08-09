using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using StructureMap;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Unit : Entity<Guid, UnitDto>, IUnit
    {
        private IUnitGroup _group;
        private HashSet<Shape> _shapes = new HashSet<Shape>(new ShapeComparer());

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

        public virtual void AddShape(Shape shape)
        {
            if (CanNotAddShape(shape)) return;
            AssociateShape.WithUnit(shape, this, _shapes);
        }

        public virtual bool CanNotAddShape(Shape shape)
        {
            return AssociateShape.CanNotAddShape(shape, _shapes);
        }

        public virtual IEnumerable<Shape> Shapes
        {
            get { return _shapes; }
            protected set { _shapes = new HashSet<Shape>(value); }
        }
    }
}
