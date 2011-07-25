using System;
using System.Collections.Generic;
using Informedica.GenForm.DataAccess.DataMappers;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using Package = Informedica.GenForm.Database.Package;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class PackageRepository: Repository<IPackage, Package>, IPackageRepository
    {
        #region Overrides of Repository<IPackage,Package>

        public override IEnumerable<IPackage> Fetch(int id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IPackage> Fetch(string name)
        {
            throw new NotImplementedException();
        }

        public override void Insert(IPackage item)
        {
            InsertUsingMapper<PackageMapper>(item);
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override void Delete(IPackage item)
        {
            throw new NotImplementedException();
        }

        protected override void InsertOnSubmit(GenFormDataContext ctx, Package dao)
        {
            ctx.Package.InsertOnSubmit(dao);
        }

        #endregion
    }
}
