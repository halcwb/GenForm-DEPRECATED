using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Unit = Informedica.GenForm.Database.Unit;

namespace Informedica.GenForm.DataAccess.Repositories.Delegates
{
    public static class UnitDelegates
    {
        public static void InsertOnSubmit(GenFormDataContext context, Unit item)
        {
            context.Unit.InsertOnSubmit(item);
        }

        public static void UpdateBo(IUnit bo, Unit dao)
        {
            bo.UnitId = dao.UnitId;
        }
    }
}