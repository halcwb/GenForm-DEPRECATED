using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using NHibernate;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class RouteRepository : NHibernateRepository<Route>
    {
        public RouteRepository(ISessionFactory factory) : base(factory) {}

        public override void Add(Route item)
        {
            base.Add(item, new RouteComparer());
        }
    }
}