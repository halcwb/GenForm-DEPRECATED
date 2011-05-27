using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.Services
{
    public interface IProductServices
    {
        IProduct GetProduct(Int32 productId);
        IProduct SaveProduct(IProduct product);
        void DeleteProduct(Int32 productId);
        IProduct GetEmptyProduct();
    }
}
