using System;
using System.Collections.Generic;
using Informedica.GenForm.DataAccess.DataMappers;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using Brand = Informedica.GenForm.Database.Brand;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class BrandRepository: Repository<IBrand, Brand>, IBrandRepository
    {
        #region Implementation of IRepository<IBrand>

        public override IEnumerable<IBrand> Fetch(int id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IBrand> Fetch(string name)
        {
            throw new NotImplementedException();
        }

        public override void Insert(IBrand item)
        {
            Insert<BrandMapper>(item);
        }

        protected override void InsertOnSubmit(GenFormDataContext ctx, Brand dao)
        {
            ctx.Brand.InsertOnSubmit(dao);
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override void Delete(IBrand item)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
