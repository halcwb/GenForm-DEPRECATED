using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products.Interfaces;

namespace Informedica.GenForm.Library.DomainModel.Products.Collections
{
    internal class UnitSet : EntitySet<Unit, UnitGroup>
    {
        internal UnitSet(ISet<Unit> set, UnitGroup parent) : 
            base(set, parent, new UnitComparer()) {}

        internal UnitSet(UnitGroup parent) :
            base(new HashedSet<Unit>(), parent, new UnitComparer()) { }

        internal void Add(IUnit unit)
        {
            base.Add((Unit)unit, ((Unit)unit).ChangeUnitGroup);
        }

        internal void Remove(IUnit unit)
        {
            base.Remove((Unit)unit);
        }
    
    }
}