using System;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.DataMappers
{
    public class BrandMapper: IDataMapper<IBrand, Database.Brand>
    {
        #region Implementation of IDataMapper<IBrand,Brand>

        public void MapFromBoToDao(IBrand bo, Database.Brand dao)
        {
            dao.BrandName = bo.BrandName == String.Empty ? null: bo.BrandName;
        }

        public void MapFromDaoToBo(Database.Brand dao, IBrand bo)
        {
            bo.BrandName = dao.BrandName ?? String.Empty;
        }

        #endregion
    }

}
