using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.DomainModel.Equality
{
    public class RouteComparer : NameComparer, IEqualityComparer<Route>
    {
        public bool Equals(Route x, Route y)
        {
            return EqualName(x.Name, y.Name) || EqualName(x.Abbreviation, y.Abbreviation);
        }

        public int GetHashCode(Route obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}