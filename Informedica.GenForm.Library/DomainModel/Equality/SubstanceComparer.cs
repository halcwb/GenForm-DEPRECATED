using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.DomainModel.Equality
{
    public class SubstanceComparer : NameComparer, IEqualityComparer<Substance>, System.Collections.IEqualityComparer
    {
        public bool Equals(Substance x, Substance y)
        {
            var areEqual = EqualName(x.Name, y.Name);
            if (x.SubstanceGroup != null && y.SubstanceGroup != null)
            {
                areEqual = areEqual && EqualName(x.SubstanceGroup.Name, y.SubstanceGroup.Name);
            }
            else
            {
                areEqual = areEqual && x.SubstanceGroup == null && y.SubstanceGroup == null;
            }

            return areEqual;
        }

        public int GetHashCode(Substance obj)
        {
            return obj.Name.GetHashCode();
        }

        public new bool Equals(object x, object y)
        {
            if (!(x is Substance && y is Substance)) return true;
            return Equals((Substance) x, (Substance) y);
        }

        public int GetHashCode(object obj)
        {
            return obj.GetHashCode();
        }
    }
}