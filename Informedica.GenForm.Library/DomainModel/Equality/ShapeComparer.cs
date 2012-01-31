using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Products.Interfaces;

namespace Informedica.GenForm.Library.DomainModel.Equality
{
    public class ShapeComparer : NameComparer, IEqualityComparer<IShape>, System.Collections.IEqualityComparer
    {
        public bool Equals(IShape x, IShape y)
        {
            return EqualName(x.Name, y.Name);
        }

        public int GetHashCode(IShape obj)
        {
            return obj.Name.GetHashCode();
        }

        public new bool Equals(object x, object y)
        {
            if (!(x is IShape) || !(y is IShape)) return true;
            return Equals((IShape)x, (IShape)y);
        }

        public int GetHashCode(object obj)
        {
            return GetHashCode((IShape) obj);
        }
    }
}