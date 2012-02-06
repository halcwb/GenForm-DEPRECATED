using System;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using NHibernate;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class UnitGroupRepository: Informedica.DataAccess.Repositories.NHibernateRepository<UnitGroup, Guid>, IRepository<UnitGroup>
    {
        public UnitGroupRepository(ISessionFactory sessionFactory): base(sessionFactory) {}

        public override void Add(UnitGroup item)
        {
            base.Add(item, new UnitGroupComparer());
        }

        #region Implementation of IRepository<UnitGroup>

        public UnitGroup GetByName(string name)
        {
            return this.SingleOrDefault(ug => ug.Name == name);
        }

        #endregion
    }
}