using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Package = Informedica.GenForm.Database.Package;

namespace Informedica.GenForm.DataAccess.Repositories.Delegates
{
    public static class PackageDelegates
    {
        public static void InsertOnSubmit(GenFormDataContext context, Package item)
        {
            context.Package.InsertOnSubmit(item);
        }

        public static void UpdateBo(IPackage bo, Package dao)
        {
            bo.PackageId = dao.PackageId;
        }

    }
}