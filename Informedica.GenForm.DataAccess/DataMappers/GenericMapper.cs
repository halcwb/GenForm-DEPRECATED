using System;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Substance = Informedica.GenForm.Database.Substance;

namespace Informedica.GenForm.DataAccess.DataMappers
{
    public class GenericMapper: IDataMapper<IGeneric, Substance>
    {
        #region Implementation of IDataMapper<IGeneric,Generic>

        public void MapFromBoToDao(IGeneric bo, Substance dao)
        {
            dao.IsGeneric = true;
            dao.SubstanceName = String.IsNullOrEmpty(bo.GenericName) ? null: bo.GenericName;
        }

        public void MapFromDaoToBo(Substance dao, IGeneric bo)
        {
            bo.GenericName = dao.SubstanceName;
        }

        #endregion
    }
}
