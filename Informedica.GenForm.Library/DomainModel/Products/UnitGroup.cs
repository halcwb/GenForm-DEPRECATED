using System;
using System.Collections.Generic;
using Informedica.GenForm.DomainModel.Interfaces;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products.Collections;
using Informedica.GenForm.Library.DomainModel.Validation;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class UnitGroup : Entity<UnitGroup>, IUnitGroup
    {
        #region Private

        private UnitSet _units;
        private ShapeSet<UnitGroup> _shapes;

        #endregion

        #region Construction

        static UnitGroup()
        {
            RegisterValidationRules();
        }

        protected UnitGroup()
        {
            InitCollections();
        }

        private void InitCollections()
        {
            _units = new UnitSet(this);
            _shapes = new ShapeSet<UnitGroup>(this);
        }

        #endregion

        #region Business

        public virtual bool AllowsConversion { get; set; }

        public virtual IEnumerable<IUnit> Units
        {
            get { return _units; }
        }

        public virtual Iesi.Collections.Generic.ISet<Unit> UnitSet
        {
            get { return _units.GetEntitySet(); }
            protected set { _units = new UnitSet(value, this); }
        }

        public virtual void AddUnit(IUnit unit)
        {
            _units.Add(unit);
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
            protected set { _shapes = new ShapeSet<UnitGroup>(value, this); }
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

        public static UnitGroup Create(UnitGroupDto dto)
        {
            var group = new UnitGroup
                       {
                           AllowsConversion = dto.AllowConversion,
                           Name = dto.Name,
                       };
            Validate(group);
            return group;
        }

        #endregion

        #region Validation

        private static void RegisterValidationRules()
        {
            ValidationRulesManager.RegisterRule<UnitGroup>(x => !String.IsNullOrWhiteSpace(x.Name));
        }

        #endregion

        #region Overrides of Entity<UnitGroup,Guid>

        public override bool IsIdentical(UnitGroup entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}