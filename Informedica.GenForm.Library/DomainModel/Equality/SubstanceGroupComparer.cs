using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.DomainModel.Equality
{
    public class SubstanceGroupComparer : NameComparer,  IEqualityComparer<SubstanceGroup>
    {
        public bool Equals(SubstanceGroup x, SubstanceGroup y)
        {
            return EqualName(x.Name, y.Name);
        }

        public int GetHashCode(SubstanceGroup obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}