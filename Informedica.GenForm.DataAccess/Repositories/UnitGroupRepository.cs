using System;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using NHibernate;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class UnitGroupRepository: NHibernateRepository<UnitGroup, Guid, UnitGroupDto>
    {
        public UnitGroupRepository(ISessionFactory sessionFactory): base(sessionFactory) {}

        public override void Add(UnitGroup item)
        {
            if (this.Contains(item, new UnitGroupComparer())) throw new AddDuplicateEntityException(item);
            base.Add(item);
        }
    }

    public class AddDuplicateEntityException : Exception
    {
        public AddDuplicateEntityException(object entity): base(entity.GetType().ToString()) {}
    }
}