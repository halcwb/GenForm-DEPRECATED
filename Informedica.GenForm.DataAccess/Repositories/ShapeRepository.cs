using System;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using NHibernate;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class ShapeRepository : Informedica.DataAccess.Repositories.NHibernateRepository<Shape, Guid>, IRepository<Shape>
    {
        public ShapeRepository(ISessionFactory factory) : base(factory) {}

        public override void Add(Shape item)
        {
            base.Add(item, new ShapeComparer());
        }

        #region Implementation of IRepository<Shape>

        public Shape GetByName(string name)
        {
            return this.SingleOrDefault(s => s.Name == name);
        }

        #endregion
    }
}