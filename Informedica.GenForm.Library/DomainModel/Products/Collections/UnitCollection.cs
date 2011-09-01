using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products.Interfaces;
using Informedica.GenForm.Library.Exceptions;

namespace Informedica.GenForm.Library.DomainModel.Products.Collections
{
    internal class UnitCollection : IEnumerable<IUnit>
    {
        private static readonly UnitComparer Comparer = new UnitComparer();

        private readonly Iesi.Collections.Generic.ISet<Unit> _units = new HashedSet<Unit>();
        private readonly UnitGroup _parent;

        internal UnitCollection(UnitGroup parent)
        {
            _parent = parent;
        }

        internal UnitCollection(Iesi.Collections.Generic.ISet<Unit> units, UnitGroup parent)
        {
            _parent = parent;
            _units = units;
        }

        public bool Contains(IUnit unit)
        {
            return _units.Any(item => Comparer.Equals(item, unit));
        }

        public IEnumerator<IUnit> GetEnumerator()
        {
            return _units.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal Iesi.Collections.Generic.ISet<Unit> GetUnitSet()
        {
            return _units;
        }

        internal void Add(Unit unit)
        {
            if (_units.Contains(unit)) return;

            if (Contains(unit)) throw new CannotAddItemException<IUnit>(unit);

            _units.Add(unit);
            unit.UnitGroup = _parent;
        }

        internal void Remove(Unit unit)
        {
            if (!_units.Contains(unit)) return;
            
            _units.Remove(unit);
            unit.UnitGroup = _parent;
        }
    }
}