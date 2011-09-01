using System.Collections;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.DomainModel.Equality
{
    public class ProductComparer : NameComparer, IEqualityComparer<Product>, IEqualityComparer
    {
        public bool Equals(Product x, Product y)
        {
            return EqualName(x.Name, y.Name) || ProductsAreEqual(x, y);
        }

        private bool ProductsAreEqual(Product x, Product y)
        {
            if (!x.Shape.Equals(y.Shape)) return false;
            if (!x.Package.Equals(y.Package)) return false;
            if (!x.Quantity.Equals(y.Quantity)) return false;

            foreach (var substance in x.SubstanceList)
            {
                if (y.SubstanceList.Contains(substance)) return false;
            }

            return true;
        }

        public int GetHashCode(Product obj)
        {
            return obj.Name.GetHashCode();
        }

        public new bool Equals(object x, object y)
        {
            if (x.GetType() != typeof(Product)) return true;
            return Equals((Product) x, (Product) y);
        }

        public int GetHashCode(object obj)
        {
            return GetHashCode((Product)obj);
        }
    }
}