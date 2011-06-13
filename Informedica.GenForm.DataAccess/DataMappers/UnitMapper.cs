using System;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Unit = Informedica.GenForm.Database.Unit;

namespace Informedica.GenForm.DataAccess.DataMappers
{
    public class UnitMapper: IDataMapper<IUnit, Unit>
    {
        #region Implementation of IDataMapper<IUnit,Unit>

        public void MapFromBoToDao(IUnit bo, Unit dao)
        {
            dao.UnitName = String.IsNullOrEmpty(bo.UnitName) ? null: bo.UnitName;
            dao.UnitAbbreviation = String.IsNullOrEmpty(bo.UnitName) ? null : bo.UnitName;
            dao.Divisor = 1;
            dao.Multiplier = 1;
            
            dao.UnitGroup = new UnitGroup();
            dao.UnitGroup.UnitGroupName = "algemeen";
            dao.UnitGroup.AllowsConversion = false;
        }

        public void MapFromDaoToBo(Unit dao, IUnit bo)
        {
            bo.UnitName = dao.UnitName;
        }

        #endregion
    }
}
