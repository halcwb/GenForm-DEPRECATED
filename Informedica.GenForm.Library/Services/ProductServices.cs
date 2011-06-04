﻿using System;
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