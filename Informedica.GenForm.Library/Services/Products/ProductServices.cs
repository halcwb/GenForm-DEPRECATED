using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.Factories;
using Informedica.GenForm.Library.Repositories;

namespace Informedica.GenForm.Library.Services.Products
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
            var repository = Factory.ObjectFactory.Instance.GetInstance<IRepositoryLinqToSql<IBrand>>();
            repository.Insert(brand);
        }

        public void AddNewGeneric(IGeneric generic)
        {
            var repository = Factory.ObjectFactory.Instance.GetInstance<IRepositoryLinqToSql<IGeneric>>();
            repository.Insert(generic);
        }

        public void AddNewShape(IShape shape)
        {
            var repository = Factory.ObjectFactory.Instance.GetInstance<IRepositoryLinqToSql<IShape>>();
            repository.Insert(shape);
        }

        public void AddNewPackage(IPackage package)
        {
            var repository = Factory.ObjectFactory.Instance.GetInstance<IRepositoryLinqToSql<IPackage>>();
            repository.Insert(package);
        }

        public void AddNewUnit(IUnit unit)
        {
            var repository = Factory.ObjectFactory.Instance.GetInstance<IRepositoryLinqToSql<IUnit>>();
            repository.Insert(unit);
        }

        public void AddNewSubstance(SubstanceDto substDto)
        {
            var subst = Substance.Create(substDto);
            var repository = Factory.ObjectFactory.Instance.GetInstance<IRepositoryLinqToSql<ISubstance>>();
            repository.Insert(subst);
        }

        public ProductDto SaveProduct(ProductDto productDto)
        {
            var repository = Factory.ObjectFactory.Instance.GetInstance<IRepositoryLinqToSql<IProduct>>();
            var product = NewProduct(productDto);
            repository.Insert(product);
            return productDto;
        }

        private static IProduct NewProduct(ProductDto productDto)
        {
            return Factory.ObjectFactory.Instance.With(productDto).GetInstance<IProduct>();
        }

        public void DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public IProduct GetEmptyProduct()
        {
            return Factory.ObjectFactory.Instance.GetInstance<IProduct>();
        }

        #endregion
    }
}