using System;
using System.Collections.Generic;
using Informedica.GenForm.DataAccess.DataMappers;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using Unit = Informedica.GenForm.Database.Unit;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class UnitRepository: Repository<IUnit, Unit>, IUnitRepository
    {
        #region Overrides of Repository<IUnit,Unit>

        public override IEnumerable<IUnit> Fetch(int id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IUnit> Fetch(string name)
        {
            throw new NotImplementedException();
        }

        public override void Insert(IUnit item)
        {
            Insert<UnitMapper>(item);
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override void Delete(IUnit item)
        {
            throw new NotImplementedException();
        }

        protected override void InsertOnSubmit(GenFormDataContext ctx, Unit dao)
        {
            ctx.Unit.InsertOnSubmit(dao);
        }

        #endregion
    }
}
