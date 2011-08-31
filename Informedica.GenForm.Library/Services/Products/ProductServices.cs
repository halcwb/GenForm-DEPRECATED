using System;
using System.Collections.Generic;
using System.Threading;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Factories;

namespace Informedica.GenForm.Library.Services.Products
{
    public class ProductServices : ServicesBase<Product, ProductDto>
    {
        private static ProductServices _instance;
        private static readonly object LockThis = new object();

        private static ProductServices Instance
        {
            get
            {
                if (_instance == null)
                    lock (LockThis)
                    {
                        if (_instance == null)
                        {
                            var instance = new ProductServices();
                            Thread.MemoryBarrier();
                            _instance = instance;
                        }
                    }
                return _instance;
            }
        }

        public static ProductFactory WithDto(ProductDto productDto)
        {
            return (ProductFactory)Instance.GetFactory(productDto);
        }

        public static IEnumerable<Product> Products
        {
            get { return Instance.Repository; }
        }

        public static void Delete(Product product)
        {
            Instance.DeleteProduct(product);
        }

        private void DeleteProduct(Product product)
        {
            Repository.Remove(product);
        }
    }
}