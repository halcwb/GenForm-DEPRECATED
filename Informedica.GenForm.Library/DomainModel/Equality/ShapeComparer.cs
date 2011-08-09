using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.DomainModel.Equality
{
    public class ShapeComparer : NameComparer, IEqualityComparer<Shape>
    {
        public bool Equals(Shape x, Shape y)
        {
            return x.Equals(y) || EqualName(x.Name, y.Name);
        }

        public int GetHashCode(Shape obj)
        {
            return obj.GetHashCode();
        }
    }
}