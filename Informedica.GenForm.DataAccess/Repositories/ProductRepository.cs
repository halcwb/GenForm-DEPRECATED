using System;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using NHibernate;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class ProductRepository : Informedica.DataAccess.Repositories.NHibernateRepository<Product, Guid>, IRepository<Product>
    {
        public ProductRepository(ISessionFactory factory) : base(factory) {}

        public override void Add(Product item)
        {
            base.Add(item, new ProductComparer());
        }

        #region Implementation of IRepository<Product>

        public Product GetByName(string name)
        {
            return this.SingleOrDefault(p => p.Name == name);
        }

        #endregion
    }
}