using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Package = Informedica.GenForm.Database.Package;

namespace Informedica.GenForm.DataAccess.Repositories.Delegates
{
    public class PackageDelegates: RepositoryDelegates<IPackage, Package>
    {
        #region Singleton

        private PackageDelegates() { }

        private static PackageDelegates Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockThis)
                    {
                        if (_instance == null) _instance = new PackageDelegates();
                    }
                }
                return (PackageDelegates)_instance;
            }
        }

        #endregion

        #region Static Access to Singleton

        public static void Insert(GenFormDataContext context, Package dao)
        {
            Instance.InsertDelegate(context, dao);
        }

        public static IEnumerable<Package> Fetch(GenFormDataContext context, Func<Package, Boolean> selector)
        {
            return Instance.FetchDelegate(context, selector);
        }

        public static void Delete(GenFormDataContext context, Func<Package, Boolean> selector)
        {
            Instance.DeleteDelegate(context, selector);
        }

        public static Func<Package, Boolean> GetIdSelector(Int32 id)
        {
            return Instance.GetIdSelectorDelegate(id);
        }

        public static Func<Package, Boolean> GetNameSelector(String name)
        {
            return Instance.GetNameSelectorDelegate(name);
        }

        #endregion

        #region Overrides of RepositoryDelegates<IPackage,Package>

        protected override void InsertDelegate(GenFormDataContext context, Package dao)
        {
            context.Package.InsertOnSubmit(dao);
        }

        protected override IEnumerable<Package> FetchDelegate(GenFormDataContext context, Func<Package, bool> selector)
        {
            return context.Package.Where(selector);
        }

        protected override void DeleteDelegate(GenFormDataContext context, Func<Package, bool> selector)
        {
            context.Package.DeleteAllOnSubmit(FetchDelegate(context, selector));
        }

        protected override Func<Package, bool> GetIdSelectorDelegate(int id)
        {
            return (package => package.PackageId == id);
        }

        protected override Func<Package, bool> GetNameSelectorDelegate(string name)
        {
            return (package => package.PackageName == name);
        }

        #endregion
    }
}