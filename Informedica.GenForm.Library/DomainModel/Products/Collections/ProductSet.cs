using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Equality;

namespace Informedica.GenForm.Library.DomainModel.Products.Collections
{
    internal class ProductSet<TParent> : EntitySet<Product, TParent>
    {
        public ProductSet(ISet<Product> set, TParent parent) : 
            base(set, parent, new ProductComparer()) {}

        public ProductSet(TParent parent) :
            base(new HashedSet<Product>(), parent, new ProductComparer()) { }
    }
}