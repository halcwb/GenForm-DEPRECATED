using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.DomainModel.Equality
{
    public class BrandComparer : IEqualityComparer<Brand>
    {
        public bool Equals(Brand x, Brand y)
        {
            return x.Equals(y) || x.Id == y.Id || x.Name == y.Name;
        }

        public int GetHashCode(Brand obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}