using System;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using NHibernate;
using StructureMap;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class UnitRepository: Informedica.DataAccess.Repositories.NHibernateRepository<Unit, Guid>, IRepository<Unit>
    {
        [DefaultConstructor]
        protected UnitRepository(): base(ObjectFactory.GetInstance<ISessionFactory>()) {}

        public UnitRepository(ISessionFactory factory) : base(factory) {}

        public override void Add(Unit item)
        {
            base.Add(item, new UnitComparer());
        }

        #region Implementation of IRepository<Unit>

        public Unit GetByName(string name)
        {
            return this.SingleOrDefault(u => u.Name == name);
        }

        #endregion
    }

}