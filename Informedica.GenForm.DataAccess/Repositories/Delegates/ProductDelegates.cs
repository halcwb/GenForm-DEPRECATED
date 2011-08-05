using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Product = Informedica.GenForm.Database.Product;

namespace Informedica.GenForm.DataAccess.Repositories.Delegates
{
    public static class ProductDelegates
    {
        public static void InsertOnSubmit(GenFormDataContext context, Product item)
        {
            context.Product.InsertOnSubmit(item);
        }

        public static void UpdateBo(IProduct bo, Product dao)
        {
            bo.ProductId = dao.ProductId;
        }

        public static IEnumerable<Product> Fetch(GenFormDataContext context, Func<Product, Boolean> selector)
        {
            return context.Product.Where(selector);
        }

        public static void Delete(GenFormDataContext context, Func<Product, Boolean> selector)
        {
            var dao = context.Product.Single(selector);
            context.Product.DeleteOnSubmit(dao);
            var productSubstances = dao.ProductSubstance;
            context.ProductSubstance.DeleteAllOnSubmit(productSubstances);
            var productRoutes = dao.ProductRoute;
            context.ProductRoute.DeleteAllOnSubmit(productRoutes);
        }

        public static Func<Product, Boolean> GetIdSelector(Int32 id)
        {
            return (p => p.ProductId == id);
        }
    }
}