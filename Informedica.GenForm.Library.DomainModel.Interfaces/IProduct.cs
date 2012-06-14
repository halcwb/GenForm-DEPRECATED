using System;
using System.Collections.Generic;

namespace Informedica.GenForm.DomainModel.Interfaces
{
    public interface IProduct
    {
        Guid Id { get; }
        String Name { get; }
        String ProductCode { get; }
        String GenericName { get; }
        IBrand Brand { get; }
        IShape Shape { get; }
        IUnitValue Quantity { get; }
        IPackage Package { get; }
        String DisplayName { get; }
        IEnumerable<IProductSubstance> Substances { get; }
        bool ContainsSubstance(ISubstance substance);
        void AddSubstance(ISubstance substance, int sortOrder, IUnitValue quanity);
        void RemoveSubstance(ISubstance substance);
        IEnumerable<IRoute> Routes { get; }
        bool ContainsRoute(IRoute route);
        void AddRoute(IRoute route);
        void RemoveRoute(IRoute route);
    }
}
