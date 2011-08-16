using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.DomainModel.Equality
{
    public class UnitValueComparer : NameComparer, IEqualityComparer<UnitValue>
    {
        public bool Equals(UnitValue x, UnitValue y)
        {
            return x.Unit.Name == y.Unit.Name && x.Value == y.Value;
        }

        public int GetHashCode(UnitValue obj)
        {
            return obj.GetHashCode();
        }
    }
}