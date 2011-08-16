using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.DomainModel.Equality
{
    internal class SubstanceComparer : NameComparer, IEqualityComparer<Substance>
    {
        public bool Equals(Substance x, Substance y)
        {
            return x.Equals(y) || EqualName(x.Name, y.Name);
        }

        public int GetHashCode(Substance obj)
        {
            return obj.GetHashCode();
        }
    }
}