using System;
using System.Collections.Generic;
using Informedica.GenForm.DataAccess.DataMappers;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using Shape = Informedica.GenForm.Database.Shape;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class ShapeRepository: Repository<IShape, Shape>, IShapeRepository
    {
        #region Overrides of Repository<IShape,Shape>

        public override IEnumerable<IShape> Fetch(int id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IShape> Fetch(string name)
        {
            throw new NotImplementedException();
        }

        public override void Insert(IShape item)
        {
            InsertUsingMapper<ShapeMapper>(item);
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override void Delete(IShape item)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateBo(IShape item, Shape dao)
        {
            item.ShapeId = dao.ShapeId;
        }

        protected override void InsertOnSubmit(GenFormDataContext ctx, Shape dao)
        {
            ctx.Shape.InsertOnSubmit(dao);
        }

        #endregion
    }
}
