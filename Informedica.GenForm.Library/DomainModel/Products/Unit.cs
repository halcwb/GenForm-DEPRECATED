using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products.Interfaces;
using Informedica.GenForm.Library.DomainModel.Validation;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Unit : Entity<Unit>, IUnit
    {
        #region Private

        private UnitGroup _unitGroup;

        #endregion

        #region Construction

        static Unit()
        {
            RegisterValidationRules();
        }

        protected Unit()
        {
        }

        private void SetUnitGroup(UnitGroup group)
        {
            ChangeUnitGroup(group);
        }

        #endregion

        #region Business

        internal protected virtual void ChangeName(string newName)
        {
            Name = newName;
        }

        public virtual String Abbreviation { get; set; }

        public virtual Decimal Multiplier { get; set; }

        public virtual Boolean IsReference { get; set; }

        public virtual UnitGroup UnitGroup
        {
            get { return _unitGroup; }
            protected internal set { _unitGroup = value; }
        }

        public virtual void ChangeUnitGroup(UnitGroup newGroup)
        {
            if (newGroup == null || newGroup == UnitGroup) return;

            _unitGroup = newGroup;
            newGroup.AddUnit(this);
        }

        public virtual bool CannotChangeGroup(UnitGroup newGroup)
        {
            return UnitGroup.Equals(newGroup, UnitGroup, new UnitGroupComparer());
        }

        internal protected virtual void RemoveFromGroup()
        {
            UnitGroup.RemoveUnit(this);
        }

        #endregion

        #region Factory

        public static Unit Create(UnitDto dto, UnitGroup group)
        {
            var unit = new Unit
            {
                Abbreviation = dto.Abbreviation,
                IsReference = dto.IsReference,
                Name = dto.Name,
                Multiplier = dto.Multiplier
            };

            unit.SetUnitGroup(group);
            Validate(unit);
            return unit;
        }

        #endregion

        #region Validation

        private static void RegisterValidationRules()
        {
            ValidationRulesManager.RegisterRule<Unit>(x => !String.IsNullOrWhiteSpace(x.Name), "Unit moet een naam hebben");
            ValidationRulesManager.RegisterRule<Unit>(x => !String.IsNullOrWhiteSpace(x.Abbreviation), "Unit moet een afkorting hebben");
            ValidationRulesManager.RegisterRule<Unit>(x => x.Multiplier > 0, "De factor moet groter dan 0 zijn");
            ValidationRulesManager.RegisterRule<Unit>(x => x.UnitGroup != null, "Unit moet in een unitgroep zitten");
        }

        #endregion

    }
}
