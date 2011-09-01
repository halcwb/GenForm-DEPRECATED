using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Equality;

namespace Informedica.GenForm.Library.DomainModel.Products.Collections
{
    internal class RouteSet<TParent> : EntitySet<Route, TParent>
    {
        internal RouteSet(ISet<Route> set, TParent parent) : 
            base(set, parent, new RouteComparer()) {}

        internal RouteSet(TParent parent) :
            base(new HashedSet<Route>(), parent, new RouteComparer()) {}

    }
}