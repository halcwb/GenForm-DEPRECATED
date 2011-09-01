using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Equality;

namespace Informedica.GenForm.Library.DomainModel.Products.Collections
{
    internal class RouteCollection<TParent> : EntityCollection<Route, TParent>
    {
        internal RouteCollection(ISet<Route> set, TParent parent) : 
            base(set, parent, new RouteComparer()) {}

        internal RouteCollection(TParent parent) :
            base(new HashedSet<Route>(), parent, new RouteComparer()) {}

    }
}