using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Product = Informedica.GenForm.Database.Product;

namespace Informedica.GenForm.DataAccess.Repositories.Delegates
{
    public class ProductDelegates: RepositoryDelegates<IProduct, Product>
    {
        #region Singleton

        private ProductDelegates() { }

        private static ProductDelegates Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockThis)
                    {
                        if (_instance == null) _instance = new ProductDelegates();
                    }
                }
                return (ProductDelegates)_instance;
            }
        }

        #endregion

        #region Static Access to Singleton

        public static void Insert(GenFormDataContext context, Product dao)
        {
            Instance.InsertDelegate(context, dao);
        }

        public static IEnumerable<Product> Fetch(GenFormDataContext context, Func<Product, Boolean> selector)
        {
            return Instance.FetchDelegate(context, selector);
        }

        public static void Delete(GenFormDataContext context, Func<Product, Boolean> selector)
        {
            Instance.DeleteDelegate(context, selector);
        }

        public static Func<Product, Boolean> GetIdSelector(Int32 id)
        {
            return Instance.GetIdSelectorDelegate(id);
        }

        public static Func<Product, Boolean> GetNameSelector(String name)
        {
            return Instance.GetNameSelectorDelegate(name);
        }

        #endregion

        #region Overrides of RepositoryDelegates<IProduct,Product>

        protected override void InsertDelegate(GenFormDataContext context, Product dao)
        {
            context.Product.InsertOnSubmit(dao);
        }

        protected override IEnumerable<Product> FetchDelegate(GenFormDataContext context, Func<Product, bool> selector)
        {
            return context.Product.Where(selector);
        }

        protected override void DeleteDelegate(GenFormDataContext context, Func<Product, bool> selector)
        {
            var dao = context.Product.Single(selector);
            context.Product.DeleteOnSubmit(dao);
            var productSubstances = dao.ProductSubstance;
            context.ProductSubstance.DeleteAllOnSubmit(productSubstances);
            var productRoutes = dao.ProductRoute;
            context.ProductRoute.DeleteAllOnSubmit(productRoutes);
        }

        protected override Func<Product, bool> GetIdSelectorDelegate(int id)
        {
            return (p => p.ProductId == id);
        }

        protected override Func<Product, bool> GetNameSelectorDelegate(string name)
        {
            return (p => p.ProductName == name);
        }

        #endregion
    }
}