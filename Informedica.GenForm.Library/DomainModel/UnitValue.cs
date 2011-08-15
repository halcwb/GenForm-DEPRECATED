using System;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.DomainModel
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
    }
}