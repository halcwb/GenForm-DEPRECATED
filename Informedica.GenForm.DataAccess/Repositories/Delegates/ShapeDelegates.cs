using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Shape = Informedica.GenForm.Database.Shape;

namespace Informedica.GenForm.DataAccess.Repositories.Delegates
{
    public static class ShapeDelegates
    {
        public static void InsertOnSubmit(GenFormDataContext context, Shape item)
        {
            context.Shape.InsertOnSubmit(item);
        }
        public static void UpdateBo(IShape bo, Shape dao)
        {
            bo.ShapeId = dao.ShapeId;
        }
    }
}