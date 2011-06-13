using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Library.DomainModel.Products;
using Shape = Informedica.GenForm.Database.Shape;

namespace Informedica.GenForm.DataAccess.DataMappers
{
    public class ShapeMapper: IDataMapper<IShape, Shape>
    {
        #region Implementation of IDataMapper<IShape,Shape>

        public void MapFromBoToDao(IShape bo, Shape dao)
        {
            dao.ShapeName = bo.ShapeName;
        }

        public void MapFromDaoToBo(Shape dao, IShape bo)
        {
            bo.ShapeName = dao.ShapeName;
        }

        #endregion
    }
}
