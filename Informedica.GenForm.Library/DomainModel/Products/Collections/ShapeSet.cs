using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Equality;

namespace Informedica.GenForm.Library.DomainModel.Products.Collections
{
    internal class ShapeSet<TParent> : EntitySet<Shape, TParent>
    {

        internal ShapeSet(ISet<Shape> value, TParent parent) : 
            base(value, parent, new ShapeComparer()) {}

        internal ShapeSet(TParent parent) : 
            base(new HashedSet<Shape>(), parent, new ShapeComparer()) {}

    }
}