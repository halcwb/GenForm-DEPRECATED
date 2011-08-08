using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Brand = Informedica.GenForm.Database.Brand;

namespace Informedica.GenForm.DataAccess.Repositories.Delegates
{
    public class BrandDelegates: RepositoryDelegates<IBrand, Brand>
    {
        #region Singleton

        private BrandDelegates() { }

        private static BrandDelegates Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockThis)
                    {
                        if (_instance == null) _instance = new BrandDelegates();
                    }
                }
                return (BrandDelegates)_instance;
            }
        }

        #endregion

        #region Static Access to Singleton

        public static void Insert(GenFormDataContext context, Brand dao)
        {
            Instance.InsertDelegate(context, dao);
        }

        public static IEnumerable<Brand> Fetch(GenFormDataContext context, Func<Brand, Boolean> selector)
        {
            return Instance.FetchDelegate(context, selector);
        }

        public static void Delete(GenFormDataContext context, Func<Brand, Boolean> selector)
        {
            Instance.DeleteDelegate(context, selector);
        }

        public static Func<Brand, Boolean> GetIdSelector(Int32 id)
        {
            return Instance.GetIdSelectorDelegate(id);
        }

        public static Func<Brand, Boolean> GetNameSelector(String name)
        {
            return Instance.GetNameSelectorDelegate(name);
        }

        #endregion

        #region Overrides of RepositoryDelegates<IBrand,Brand>

        protected override void InsertDelegate(GenFormDataContext context, Brand dao)
        {
            context.Brand.InsertOnSubmit(dao);
        }

        protected override IEnumerable<Brand> FetchDelegate(GenFormDataContext context, Func<Brand, bool> selector)
        {
            return context.Brand.Where(selector);
        }

        protected override void DeleteDelegate(GenFormDataContext context, Func<Brand, bool> selector)
        {
            context.Brand.DeleteAllOnSubmit(FetchDelegate(context, selector));
        }

        protected override Func<Brand, bool> GetIdSelectorDelegate(int id)
        {
            return (brand => brand.BrandId == id);
        }

        protected override Func<Brand, bool> GetNameSelectorDelegate(string name)
        {
            return (brand => brand.BrandName == name);
        }

        #endregion
    }
}