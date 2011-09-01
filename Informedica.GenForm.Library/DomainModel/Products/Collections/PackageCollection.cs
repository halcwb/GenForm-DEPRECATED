using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Equality;

namespace Informedica.GenForm.Library.DomainModel.Products.Collections
{
    internal class PackageCollection : EntityCollection<Package, Shape>
    {
        internal PackageCollection(ISet<Package> set, Shape parent) : 
            base(set, parent, new PackageComparer()) {}

        internal PackageCollection(Shape parent) :
            base(new HashedSet<Package>(), parent, new PackageComparer()) {}

        internal void Add(Package package)
        {
            base.Add(package, package.AddShape);
        }

        internal void Remove(Package package)
        {
            base.Remove(package, package.RemoveShape);
        }
    }
}