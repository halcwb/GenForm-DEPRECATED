using System;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.Services
{
    public interface IProductServices
    {
        IProduct GetProduct(Int32 productId);
        void SaveProduct(IProduct product);
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

    public interface ISubstance
    {
        String SubstanceName { get; set; }
    }
}
