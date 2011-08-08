using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Users;

namespace Informedica.GenForm.DataAccess.Repositories.Delegates
{
    public class UserDelegates: RepositoryDelegates<IUser, GenFormUser>
    {
        #region Singleton

        private UserDelegates() { }

        private static UserDelegates Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockThis)
                    {
                        if (_instance == null) _instance = new UserDelegates();
                    }
                }
                return (UserDelegates)_instance;
            }
        }

        #endregion

        #region Static Access to Singleton

        public static void Insert(GenFormDataContext context, GenFormUser dao)
        {
            Instance.InsertDelegate(context, dao);
        }

        public static IEnumerable<GenFormUser> Fetch(GenFormDataContext context, Func<GenFormUser, Boolean> selector)
        {
            return Instance.FetchDelegate(context, selector);
        }

        public static void Delete(GenFormDataContext context, Func<GenFormUser, Boolean> selector)
        {
            Instance.DeleteDelegate(context, selector);
        }

        public static Func<GenFormUser, Boolean> GetIdSelector(Int32 id)
        {
            return Instance.GetIdSelectorDelegate(id);
        }

        public static Func<GenFormUser, Boolean> GetNameSelector(String name)
        {
            return Instance.GetNameSelectorDelegate(name);
        }

        #endregion

        #region Overrides of RepositoryDelegates<IUser,User>

        protected override void InsertDelegate(GenFormDataContext context, GenFormUser dao)
        {
            context.GenFormUser.InsertOnSubmit(dao);
        }

        protected override IEnumerable<GenFormUser> FetchDelegate(GenFormDataContext context, Func<GenFormUser, bool> selector)
        {
            return context.GenFormUser.Where(selector);
        }

        protected override void DeleteDelegate(GenFormDataContext context, Func<GenFormUser, bool> selector)
        {
            context.GenFormUser.DeleteAllOnSubmit(FetchDelegate(context, selector));
        }

        protected override Func<GenFormUser, bool> GetIdSelectorDelegate(int id)
        {
            return (user => user.UserId == id);
        }

        protected override Func<GenFormUser, bool> GetNameSelectorDelegate(string name)
        {
            return (user => user.UserName == name);
        }

        #endregion
    }
}
