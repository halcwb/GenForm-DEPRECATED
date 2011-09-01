using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Equality;

namespace Informedica.GenForm.Library.DomainModel.Products.Collections
{
    internal class ShapeCollection<TParent> : EntityCollection<Shape, TParent>
    {

        internal ShapeCollection(ISet<Shape> value, TParent parent) : 
            base(value, parent, new ShapeComparer()) {}

        internal ShapeCollection(TParent parent) : 
            base(new HashedSet<Shape>(), parent, new ShapeComparer()) {}

    }
}