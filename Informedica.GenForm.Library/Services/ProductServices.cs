using System;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using StructureMap;

namespace Informedica.GenForm.Library.Services
{
    public class ProductServices : IProductServices
    {
        #region Implementation of IProductServices

        public IProduct GetProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public IProduct GetProduct(String productName)
        {
            throw new NotImplementedException();
        }

        public void AddNewBrand(IBrand brand)
        {
            var repository = ObjectFactory.GetInstance<IBrandRepository>();
            repository.Insert(brand);
        }

        public void AddNewGeneric(IGeneric generic)
        {
            var repository = ObjectFactory.GetInstance<IGenericRepository>();
            repository.Insert(generic);
        }

        public void AddNewShape(IShape shape)
        {
            var repository = ObjectFactory.GetInstance<IShapeRepository>();
            repository.Insert(shape);
        }

        public void AddNewPackage(IPackage package)
        {
            var repository = ObjectFactory.GetInstance<IPackageRepository>();
            repository.Insert(package);
        }

        public void AddNewUnit(IUnit unit)
        {
            var repository = ObjectFactory.GetInstance<IUnitRepository>();
            repository.Insert(unit);
        }

        public void AddNewSubstance(ISubstance subst)
        {
            var repository = ObjectFactory.GetInstance<ISubstanceRepository>();
            repository.Insert(subst);
        }

        public void SaveProduct(IProduct product)
        {
            var repository = ObjectFactory.GetInstance<IProductRepository>();
            repository.Insert(product);
        }

        public void DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public IProduct GetEmptyProduct()
        {
            return new Product();
        }

        #endregion
    }
}