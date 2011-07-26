using System;
using System.Collections.Generic;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using Substance = Informedica.GenForm.Database.Substance;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class SubstanceRepository:Repository<ISubstance, Database.Substance>, ISubstanceRepository
    {
        #region Overrides of Repository<ISubstance,Substance>

        public override IEnumerable<ISubstance> Fetch(int id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<ISubstance> Fetch(string name)
        {
            throw new NotImplementedException();
        }

        public override void Insert(ISubstance item)
        {
            InsertUsingMapper<IDataMapper<ISubstance, Substance>>(item);
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override void Delete(ISubstance item)
        {
            throw new NotImplementedException();
        }

        protected override void InsertOnSubmit(GenFormDataContext ctx, Substance dao)
        {
            ctx.Substance.InsertOnSubmit(dao);
        }

        #endregion
    }
}