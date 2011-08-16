using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using NHibernate;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class UnitGroupRepository: NHibernateRepository<UnitGroup, Guid, UnitGroupDto>
    {
        public UnitGroupRepository(ISessionFactory sessionFactory): base(sessionFactory) {}

        public override void Add(UnitGroup item)
        {
            base.Add(item, new UnitGroupComparer());
        }
    }
}