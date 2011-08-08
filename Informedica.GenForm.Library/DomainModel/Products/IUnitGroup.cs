using System;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public interface IUnitGroup
    {
        Guid Id { get; }
        String UnitGroupName { get; set; }
        Boolean AllowsConversion { get; set; }
    }
}