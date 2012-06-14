using Iesi.Collections.Generic;
using Informedica.GenForm.DomainModel.Interfaces;
using Informedica.GenForm.Library.DomainModel.Equality;

namespace Informedica.GenForm.Library.DomainModel.Products.Collections
{
    internal class SubstanceSet : EntitySet<Substance, SubstanceGroup>
    {
        public SubstanceSet(ISet<Substance> set, SubstanceGroup parent) : 
            base(set, parent, new SubstanceComparer()) {}

        public SubstanceSet(SubstanceGroup parent) :
            base(new HashedSet<Substance>(), parent, new SubstanceComparer()) { }

        internal void Add(ISubstance substance)
        {
            base.Add((Substance)substance, ((Substance)substance).SetSubstanceGroup);
        }

        internal void Remove(ISubstance substance)
        {
            base.Remove((Substance)substance, ((Substance)substance).RemoveFromSubstanceGroup);
        }

    }
}