using System;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public interface IUnit
    {
        String Name { get; set; }
        IUnitGroup UnitGroup { get; set; }
        String Abbreviation { get; set; }
        Decimal Multiplier { get; set; }
        Boolean IsReference { get; set; }
    }
}
