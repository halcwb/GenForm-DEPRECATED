using System.Collections;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.DomainModel.Equality
{
    public class ProductComparer : NameComparer, IEqualityComparer<Product>, IEqualityComparer
    {
        public bool Equals(Product x, Product y)
        {
            return x.Equals(y) || EqualName(x.Name, y.Name);
        }

        public int GetHashCode(Product obj)
        {
            return obj.GetHashCode();
        }

        public bool Equals(object x, object y)
        {
            if (x.GetType() == typeof(Product)) return Equals((Product) x, (Product) y);
            
            return true;
        }

        public int GetHashCode(object obj)
        {
            return GetHashCode((Product)obj);
        }
    }
}