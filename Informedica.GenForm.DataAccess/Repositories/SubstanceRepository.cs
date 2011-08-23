using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using NHibernate;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class SubstanceRepository : NHibernateRepository<Substance, Guid, SubstanceDto>
    {
        public SubstanceRepository(ISessionFactory factory) : base(factory) {}


        public override void Add(Substance item)
        {
            base.Add(item, new SubstanceComparer());
        }
    }
}