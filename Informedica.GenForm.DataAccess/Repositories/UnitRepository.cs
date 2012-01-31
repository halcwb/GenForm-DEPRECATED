using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using NHibernate;
using StructureMap;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class UnitRepository: NHibernateRepository<Unit>
    {
        [DefaultConstructor]
        protected UnitRepository(): base(ObjectFactory.GetInstance<ISessionFactory>()) {}

        public UnitRepository(ISessionFactory factory) : base(factory) {}

        public override void Add(Unit item)
        {
            base.Add(item, new UnitComparer());
        }
    }

}