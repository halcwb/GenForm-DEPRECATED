using System;
using System.Collections.Generic;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public interface IUnitGroup
    {
        Guid Id { get; }
        String Name { get; }
        Boolean AllowsConversion { get; }
        IEnumerable<Unit> Units { get; }
        void RemoveUnit(Unit unit);
        void AddUnit(Unit unit);
    }
}