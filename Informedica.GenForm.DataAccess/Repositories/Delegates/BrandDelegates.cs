using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Brand = Informedica.GenForm.Database.Brand;

namespace Informedica.GenForm.DataAccess.Repositories.Delegates
{
    public static class BrandDelegates
    {
        public static void InsertOnSubmit(GenFormDataContext context, Brand item)
        {
            context.Brand.InsertOnSubmit(item);
        }

        public static void UpdateBo(IBrand bo, Brand dao)
        {
            bo.BrandId = dao.BrandId;
        }

    }
}