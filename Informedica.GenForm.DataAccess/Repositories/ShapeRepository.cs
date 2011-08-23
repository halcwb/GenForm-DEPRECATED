using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using NHibernate;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class ShapeRepository : NHibernateRepository<Shape, Guid, ShapeDto>
    {
        public ShapeRepository(ISessionFactory factory) : base(factory) {}

        public override void Add(Shape item)
        {
            base.Add(item, new ShapeComparer());
        }
    }
}