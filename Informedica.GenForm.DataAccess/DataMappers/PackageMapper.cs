using System;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Library.DomainModel.Products;
using Package = Informedica.GenForm.Database.Package;

namespace Informedica.GenForm.DataAccess.DataMappers
{
    public class PackageMapper: IDataMapper<IPackage, Database.Package>
    {
        #region Implementation of IDataMapper<IPackage,Package>

        public void MapFromBoToDao(IPackage bo, Package dao)
        {
            dao.PackageName = String.IsNullOrEmpty(bo.PackageName) ? null: bo.PackageName;
        }

        public void MapFromDaoToBo(Package dao, IPackage bo)
        {
            bo.PackageName = dao.PackageName;
        }

        #endregion
    }
}
