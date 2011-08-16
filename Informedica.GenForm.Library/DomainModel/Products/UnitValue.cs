using System;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class UnitValue
    {
        protected UnitValue() {}

        public UnitValue(Decimal value, Unit unit)
        {
            SetValue(value, unit);
        }

        private void SetValue(decimal value, Unit unit)
        {
            Value = value;
            Unit = unit;
        }

        public virtual Unit Unit { get; protected set; }

        public virtual decimal Value { get; protected set; }

        public static UnitValue Create(decimal quantity, Unit unit)
        {
            return new UnitValue(quantity, unit);
        }
    }
}