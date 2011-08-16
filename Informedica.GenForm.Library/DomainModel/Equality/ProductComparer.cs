using System;
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

        public new bool Equals(object x, object y)
        {
            if (x is Product) return Equals((Product) x, (Product) y);
            if (x is string) return EqualName((String)x, (String)y);
            if (x is Shape) return new ShapeComparer().Equals((Shape) x, (Shape) y);
            if (x is Package) return new PackageComparer().Equals((Package)x, (Package)y);
            if (x is UnitValue) return new UnitValueComparer().Equals((UnitValue)x, (UnitValue)y);
            
            return true;
        }

        public int GetHashCode(object obj)
        {
            return GetHashCode((Product)obj);
        }
    }
}