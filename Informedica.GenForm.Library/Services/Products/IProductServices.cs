using System;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Services.Products.dto;

namespace Informedica.GenForm.Library.Services.Products
{
    public interface IProductServices
    {
        IProduct GetProduct(Int32 productId);
        ProductDto SaveProduct(ProductDto product);
        void DeleteProduct(Int32 productId);
        IProduct GetEmptyProduct();
        IProduct GetProduct(String productName);
        void AddNewBrand(IBrand brand);
        void AddNewGeneric(IGeneric generic);
        void AddNewShape(IShape shape);
        void AddNewPackage(IPackage package);
        void AddNewUnit(IUnit unit);
        void AddNewSubstance(ISubstance subst);
    }
}
