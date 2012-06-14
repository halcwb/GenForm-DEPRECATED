using System.Collections.Generic;
using Informedica.GenForm.DomainModel.Interfaces;

namespace Informedica.GenForm.Library.DomainModel.Equality
{
    public class UnitComparer : NameComparer, IEqualityComparer<IUnit>, System.Collections.IEqualityComparer
    {
        public bool Equals(IUnit x, IUnit y)
        {
            return EqualName(x.Name, y.Name) || EqualName(x.Abbreviation, y.Abbreviation) ;
        }

        public int GetHashCode(IUnit obj)
        {
            return obj.Name.GetHashCode();
        }

        public new bool Equals(object x, object y)
        {
            return Equals((IUnit) x, (IUnit) y);
        }

        public int GetHashCode(object obj)
        {
            return GetHashCode((IUnit) obj);
        }
    }
}