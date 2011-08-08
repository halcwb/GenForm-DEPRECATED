using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Substance = Informedica.GenForm.Database.Substance;

namespace Informedica.GenForm.DataAccess.Repositories.Delegates
{
    public class SubstanceDelegates: RepositoryDelegates<ISubstance, Substance>
    {
        #region Singleton

        private SubstanceDelegates() { }

        private static SubstanceDelegates Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockThis)
                    {
                        if (_instance == null) _instance = new SubstanceDelegates();
                    }
                }
                return (SubstanceDelegates)_instance;
            }
        }

        #endregion

        #region Static Access to Singleton

        public static void Insert(GenFormDataContext context, Substance dao)
        {
            Instance.InsertDelegate(context, dao);
        }

        public static IEnumerable<Substance> Fetch(GenFormDataContext context, Func<Substance, Boolean> selector)
        {
            return Instance.FetchDelegate(context, selector);
        }

        public static void Delete(GenFormDataContext context, Func<Substance, Boolean> selector)
        {
            Instance.DeleteDelegate(context, selector);
        }

        public static Func<Substance, Boolean> GetIdSelector(Int32 id)
        {
            return Instance.GetIdSelectorDelegate(id);
        }

        public static Func<Substance, Boolean> GetNameSelector(String name)
        {
            return Instance.GetNameSelectorDelegate(name);
        }

        #endregion

        #region Overrides of RepositoryDelegates<ISubstance,Substance>

        protected override void InsertDelegate(GenFormDataContext context, Substance dao)
        {
            context.Substance.InsertOnSubmit(dao);
        }

        protected override IEnumerable<Substance> FetchDelegate(GenFormDataContext context, Func<Substance, bool> selector)
        {
            return context.Substance.Where(selector);
        }

        protected override void DeleteDelegate(GenFormDataContext context, Func<Substance, bool> selector)
        {
            context.Substance.DeleteAllOnSubmit(FetchDelegate(context, selector));
        }

        protected override Func<Substance, bool> GetIdSelectorDelegate(int id)
        {
            return (substance => substance.SubstanceId == id);
        }

        protected override Func<Substance, bool> GetNameSelectorDelegate(string name)
        {
            return (substance => substance.SubstanceName == name);
        }

        #endregion
    }
}