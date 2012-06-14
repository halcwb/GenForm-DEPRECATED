using System;
using System.Collections.Generic;

namespace Informedica.GenForm.DomainModel.Interfaces
{
    public interface IShape
    {
        Guid Id { get; }
        String Name { get; }
        IEnumerable<IProduct> Products { get; }
        IEnumerable<IPackage> Packages { get; }
        IEnumerable<IUnitGroup> UnitGroups { get; }
    }
}
