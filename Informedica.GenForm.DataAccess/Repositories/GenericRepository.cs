using System;
using System.Collections.Generic;
using Informedica.GenForm.DataAccess.DataMappers;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class GenericRepository: Repository<IGeneric, Substance>, IGenericRepository
    {
        #region Implementation of IRepository<IGeneric>

        public override IEnumerable<IGeneric> Fetch(int id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IGeneric> Fetch(string name)
        {
            throw new NotImplementedException();
        }

        public override void Insert(IGeneric item)
        {
            Insert<GenericMapper>(item);
        }

        protected override void InsertOnSubmit(GenFormDataContext ctx, Substance dao)
        {
            ctx.Substance.InsertOnSubmit(dao);
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override void Delete(IGeneric item)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
