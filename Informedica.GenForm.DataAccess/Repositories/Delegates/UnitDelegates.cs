using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Unit = Informedica.GenForm.Database.Unit;

namespace Informedica.GenForm.DataAccess.Repositories.Delegates
{
    public class UnitDelegates: RepositoryDelegates<IUnit, Unit>
    {
        #region Singleton

        private UnitDelegates() { }

        private static UnitDelegates Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockThis)
                    {
                        if (_instance == null) _instance = new UnitDelegates();
                    }
                }
                return (UnitDelegates)_instance;
            }
        }

        #endregion

        #region Static Access to Singleton

        public static void Insert(GenFormDataContext context, Unit dao)
        {
            Instance.InsertDelegate(context, dao);
        }

        public static IEnumerable<Unit> Fetch(GenFormDataContext context, Func<Unit, Boolean> selector)
        {
            return Instance.FetchDelegate(context, selector);
        }

        public static void Delete(GenFormDataContext context, Func<Unit, Boolean> selector)
        {
            Instance.DeleteDelegate(context, selector);
        }

        public static Func<Unit, Boolean> GetIdSelector(Int32 id)
        {
            return Instance.GetIdSelectorDelegate(id);
        }

        public static Func<Unit, Boolean> GetNameSelector(String name)
        {
            return Instance.GetNameSelectorDelegate(name);
        }

        #endregion

        #region Overrides of RepositoryDelegates<IUnit,Unit>

        protected override void InsertDelegate(GenFormDataContext context, Unit dao)
        {
            context.Unit.InsertOnSubmit(dao);
        }

        protected override IEnumerable<Unit> FetchDelegate(GenFormDataContext context, Func<Unit, bool> selector)
        {
            return context.Unit.Where(selector);
        }

        protected override void DeleteDelegate(GenFormDataContext context, Func<Unit, bool> selector)
        {
            context.Unit.DeleteAllOnSubmit(FetchDelegate(context, selector));
        }

        protected override Func<Unit, bool> GetIdSelectorDelegate(int id)
        {
            return (unit => unit.UnitId == id);
        }

        protected override Func<Unit, bool> GetNameSelectorDelegate(string name)
        {
            return (unit => unit.UnitName == name);
        }

        #endregion
    }
}