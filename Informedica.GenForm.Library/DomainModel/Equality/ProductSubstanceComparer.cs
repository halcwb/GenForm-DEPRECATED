using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.DomainModel.Equality
{
    public class ProductSubstanceComparer : NameComparer, IEqualityComparer<ProductSubstance>
    {
        public bool Equals(ProductSubstance x, ProductSubstance y)
        {
            if (x == null || y == null) return false;
            return EqualName(x.Name, y.Name) || EqualName(x.Product.Name, y.Product.Name);
        }

        public int GetHashCode(ProductSubstance obj)
        {
            var hash = 17;
            hash = hash + 7 * obj.Product.Name.GetHashCode();
            hash = hash + 7*obj.Product.Name.GetHashCode();
            return hash;
        }
    }
}