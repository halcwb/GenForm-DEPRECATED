using System;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using NHibernate;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class RouteRepository : Informedica.DataAccess.Repositories.NHibernateRepository<Route, Guid>, IRepository<Route>
    {
        public RouteRepository(ISessionFactory factory) : base(factory) {}

        public override void Add(Route item)
        {
            base.Add(item, new RouteComparer());
        }

        #region Implementation of IRepository<Route>

        public Route GetByName(string name)
        {
            return this.SingleOrDefault(r => r.Name == name);
        }

        #endregion
    }
}