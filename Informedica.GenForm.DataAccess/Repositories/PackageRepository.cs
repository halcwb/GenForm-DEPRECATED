using System;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using NHibernate;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class PackageRepository : Informedica.DataAccess.Repositories.NHibernateRepository<Package, Guid>, IRepository<Package>
    {
        public PackageRepository(ISessionFactory factory) : base(factory) {}

        public override void Add(Package item)
        {
            base.Add(item, new PackageComparer());
        }

        #region Implementation of IRepository<Package>

        public Package GetByName(string name)
        {
            return this.SingleOrDefault(p => p.Name == name);
        }

        #endregion
    }
}