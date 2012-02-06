using System;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using NHibernate;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class BrandRepository :Informedica.DataAccess.Repositories.NHibernateRepository<Brand, Guid>, IRepository<Brand>
    {
        private static readonly BrandComparer Comparer = new BrandComparer();

        public BrandRepository(ISessionFactory sessionFactory) : base(sessionFactory) {}

        public override void Add(Brand item)
        {
            base.Add(item, Comparer);
        }

        #region Implementation of IRepository<Brand>

        public Brand GetByName(string name)
        {
            return this.SingleOrDefault(b => b.Name == name);
        }

        #endregion
    }
}