using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.DomainModel.Equality
{
    public class RouteComparer : NameComparer, IEqualityComparer<Route>, System.Collections.IEqualityComparer
    {
        public bool Equals(Route x, Route y)
        {
            return EqualName(x.Name, y.Name) || EqualName(x.Abbreviation, y.Abbreviation);
        }

        public int GetHashCode(Route obj)
        {
            return obj.Name.GetHashCode();
        }

        public new bool Equals(object x, object y)
        {
            if (!(x is Route) || !(y is Route)) return true;
            return Equals((Route) x, (Route) y);
        }

        public int GetHashCode(object obj)
        {
            return GetHashCode((Route) obj);
        }
    }
}