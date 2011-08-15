using System;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using NHibernate;
using StructureMap;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class UnitRepository: NHibernateRepository<Unit, Guid, UnitDto>
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