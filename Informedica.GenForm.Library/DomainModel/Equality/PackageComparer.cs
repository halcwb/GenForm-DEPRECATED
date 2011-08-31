using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.DomainModel.Equality
{
    public class PackageComparer : NameComparer, IEqualityComparer<Package>
    {
        public bool Equals(Package x, Package y)
        {
            return EqualName(x.Name, y.Name) || EqualName(x.Abbreviation, y.Abbreviation);
        }

        public int GetHashCode(Package obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}