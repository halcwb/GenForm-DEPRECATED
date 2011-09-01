using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.DomainModel.Equality
{
    public class PackageComparer : NameComparer, IEqualityComparer<Package>, System.Collections.IEqualityComparer
    {
        public bool Equals(Package x, Package y)
        {
            return EqualName(x.Name, y.Name) || EqualName(x.Abbreviation, y.Abbreviation);
        }

        public int GetHashCode(Package obj)
        {
            return obj.Name.GetHashCode();
        }

        public new bool Equals(object x, object y)
        {
            if (!(x is Package) || !(y is Package)) return true;
            return Equals((Package) x, (Package) y);
        }

        public int GetHashCode(object obj)
        {
            return GetHashCode((Package) obj);
        }
    }
}