using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using NHibernate;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class BrandRepository : NHibernateRepository<Brand, Guid, BrandDto>
    {
        public BrandRepository(ISessionFactory sessionFactory) : base(sessionFactory) {}

        public override void Add(Brand item)
        {
            base.Add(item, new BrandComparer());
        }
    }
}