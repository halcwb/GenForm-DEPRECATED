using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.DomainModel.Equality
{
    public class UnitGroupComparer: NameComparer, IEqualityComparer<UnitGroup>
    {
        public bool Equals(UnitGroup x, UnitGroup y)
        {
            return x.Equals(y) || EqualName(x.Name, y.Name);
        }

        public int GetHashCode(UnitGroup obj)
        {
            return obj.GetHashCode();
        }
    }
}