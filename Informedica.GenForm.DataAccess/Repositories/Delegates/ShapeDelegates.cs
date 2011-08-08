using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Shape = Informedica.GenForm.Database.Shape;

namespace Informedica.GenForm.DataAccess.Repositories.Delegates
{
    public class ShapeDelegates: RepositoryDelegates<IShape, Shape>
    {
        #region Singleton

        private ShapeDelegates() { }

        private static ShapeDelegates Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockThis)
                    {
                        if (_instance == null) _instance = new ShapeDelegates();
                    }
                }
                return (ShapeDelegates)_instance;
            }
        }

        #endregion

        #region Static Access to Singleton

        public static void Insert(GenFormDataContext context, Shape dao)
        {
            Instance.InsertDelegate(context, dao);
        }

        public static IEnumerable<Shape> Fetch(GenFormDataContext context, Func<Shape, Boolean> selector)
        {
            return Instance.FetchDelegate(context, selector);
        }

        public static void Delete(GenFormDataContext context, Func<Shape, Boolean> selector)
        {
            Instance.DeleteDelegate(context, selector);
        }

        public static Func<Shape, Boolean> GetIdSelector(Int32 id)
        {
            return Instance.GetIdSelectorDelegate(id);
        }

        public static Func<Shape, Boolean> GetNameSelector(String name)
        {
            return Instance.GetNameSelectorDelegate(name);
        }

        #endregion

        #region Overrides of RepositoryDelegates<IShape,Shape>

        protected override void InsertDelegate(GenFormDataContext context, Shape dao)
        {
            context.Shape.InsertOnSubmit(dao);
        }

        protected override IEnumerable<Shape> FetchDelegate(GenFormDataContext context, Func<Shape, bool> selector)
        {
            return context.Shape.Where(selector);
        }

        protected override void DeleteDelegate(GenFormDataContext context, Func<Shape, bool> selector)
        {
            context.Shape.DeleteAllOnSubmit(FetchDelegate(context, selector));
        }

        protected override Func<Shape, bool> GetIdSelectorDelegate(int id)
        {
            return (shape => shape.ShapeId == id);
        }

        protected override Func<Shape, bool> GetNameSelectorDelegate(string name)
        {
            return (shape => shape.ShapeName == name);
        }

        #endregion
    }
}