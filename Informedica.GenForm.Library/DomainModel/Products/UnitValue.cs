using System;
using Informedica.GenForm.Library.DomainModel.Products.Interfaces;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class UnitValue : IUnitValue
    {
        protected UnitValue() {}

        public UnitValue(Decimal value, IUnit unit)
        {
            SetValue(value, unit);
        }

        private void SetValue(decimal value, IUnit unit)
        {
            Value = value;
            Unit = unit;
        }

        public virtual IUnit Unit { get; protected set; }

        public virtual decimal Value { get; protected set; }

        public static UnitValue Create(decimal quantity, Unit unit)
        {
            return new UnitValue(quantity, unit);
        }
    }
}