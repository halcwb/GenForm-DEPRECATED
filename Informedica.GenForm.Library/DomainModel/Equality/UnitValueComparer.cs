using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.DomainModel.Equality
{
    public class UnitValueComparer : NameComparer, IEqualityComparer<UnitValue>
    {
        public bool Equals(UnitValue x, UnitValue y)
        {
            return EqualName(x.Unit.Name, y.Unit.Name) && x.Value == y.Value;
        }

        public int GetHashCode(UnitValue obj)
        {
            var hash = 17;
            hash = hash + 7 * obj.Unit.Name.GetHashCode();
            hash = hash + 7 * obj.Value.GetHashCode();
            return hash;
        }
    }
}