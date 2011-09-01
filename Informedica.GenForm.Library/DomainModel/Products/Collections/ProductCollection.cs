using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Equality;

namespace Informedica.GenForm.Library.DomainModel.Products.Collections
{
    internal class ProductCollection<TParent> : EntityCollection<Product, TParent>
    {
        public ProductCollection(ISet<Product> set, TParent parent) : 
            base(set, parent, new ProductComparer()) {}

        public ProductCollection(TParent parent) :
            base(new HashedSet<Product>(), parent, new ProductComparer()) { }
    }
}