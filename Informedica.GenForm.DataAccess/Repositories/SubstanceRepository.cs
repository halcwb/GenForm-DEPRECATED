using System;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using NHibernate;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class SubstanceRepository : Informedica.DataAccess.Repositories.NHibernateRepository<Substance, Guid>, IRepository<Substance>
    {
        public SubstanceRepository(ISessionFactory factory) : base(factory) { }

        public override void Add(Substance item)
        {
            base.Add(item, new SubstanceComparer());
        }

        #region Implementation of IRepository<Substance>

        public Substance GetByName(string name)
        {
            return this.SingleOrDefault(s => s.Name == name);
        }

        #endregion
    }
}