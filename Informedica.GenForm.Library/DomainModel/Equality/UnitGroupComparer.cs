using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.DomainModel.Equality
{
    public class UnitGroupComparer: NameComparer, IEqualityComparer<UnitGroup>, System.Collections.IEqualityComparer
    {
        public bool Equals(UnitGroup x, UnitGroup y)
        {
            return EqualName(x.Name, y.Name);
        }

        public int GetHashCode(UnitGroup obj)
        {
            return obj.Name.GetHashCode();
        }

        public new bool Equals(object x, object y)
        {
            if (!(x is UnitGroup) || !(y is UnitGroup)) return true;
            return Equals((UnitGroup) x, (UnitGroup) y);
        }

        public int GetHashCode(object obj)
        {
            return GetHashCode((UnitGroup) obj);
        }
    }
}