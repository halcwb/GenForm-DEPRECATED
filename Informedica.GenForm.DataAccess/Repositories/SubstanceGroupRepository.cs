using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using NHibernate;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class SubstanceGroupRepository : NHibernateRepository<SubstanceGroup>
    {
        public SubstanceGroupRepository(ISessionFactory factory) : base(factory)
        {
        }

        public override void Add(SubstanceGroup item)
        {
            base.Add(item, new SubstanceGroupComparer());
        }
    }
}