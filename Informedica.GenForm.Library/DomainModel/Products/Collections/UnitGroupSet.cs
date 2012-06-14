using Iesi.Collections.Generic;
using Informedica.GenForm.DomainModel.Interfaces;
using Informedica.GenForm.Library.DomainModel.Equality;

namespace Informedica.GenForm.Library.DomainModel.Products.Collections
{
    internal class UnitGroupSet : EntitySet<UnitGroup, IShape>
    {
        internal UnitGroupSet(ISet<UnitGroup> unitGroups, IShape parent) : 
            base(unitGroups, parent, new UnitGroupComparer()) 
        {}

        internal UnitGroupSet(IShape parent) : 
            base(new HashedSet<UnitGroup>(), parent, new UnitGroupComparer())
        {}

        internal void Add(UnitGroup unitGroup)
        {
            base.Add(unitGroup, unitGroup.AddShape);
        }

        internal new void Remove(UnitGroup unitGroup)
        {
            base.Remove(unitGroup, unitGroup.RemoveShape);
        }
    }
}