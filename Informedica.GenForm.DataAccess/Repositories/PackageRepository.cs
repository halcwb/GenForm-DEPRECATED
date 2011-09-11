using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using NHibernate;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class PackageRepository : NHibernateRepository<Package>
    {
        public PackageRepository(ISessionFactory factory) : base(factory) {}

        public override void Add(Package item)
        {
            base.Add(item, new PackageComparer());
        }
    }
}