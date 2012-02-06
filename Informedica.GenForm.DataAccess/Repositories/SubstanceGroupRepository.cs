using System;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using NHibernate;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class SubstanceGroupRepository : Informedica.DataAccess.Repositories.NHibernateRepository<SubstanceGroup, Guid>, IRepository<SubstanceGroup>
    {
        public SubstanceGroupRepository(ISessionFactory factory) : base(factory)
        {
        }

        public override void Add(SubstanceGroup item)
        {
            base.Add(item, new SubstanceGroupComparer());
        }

        #region Implementation of IRepository<SubstanceGroup>

        public SubstanceGroup GetByName(string name)
        {
            return this.SingleOrDefault(sg => sg.Name == name);
        }

        #endregion
    }
}