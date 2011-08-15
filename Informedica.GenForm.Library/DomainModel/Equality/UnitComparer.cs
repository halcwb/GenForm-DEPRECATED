using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.DomainModel.Equality
{
    public class UnitComparer : NameComparer, IEqualityComparer<Unit>
    {
        public bool Equals(Unit x, Unit y)
        {
            return x.Equals(y) || EqualName(x.Name, y.Name) || EqualName(x.Abbreviation, y.Abbreviation) ;
        }

        public int GetHashCode(Unit obj)
        {
            return obj.GetHashCode();
        }
    }
}