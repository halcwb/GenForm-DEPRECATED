using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Users;

namespace Informedica.GenForm.DataAccess.Repositories.Delegates
{
    public class UserDelegates
    {
        public static void InsertOnSubmit(GenFormDataContext context, GenFormUser item)
        {
            context.GenFormUser.InsertOnSubmit(item);
        }

        public static void UpdateBo(IUser bo, GenFormUser dao)
        {
            bo.UserId = dao.UserId;
        }

        public static IEnumerable<GenFormUser> Fetch(GenFormDataContext context, Func<GenFormUser, Boolean> selector)
        {
            return context.GenFormUser.Where(selector);
        }

        public static void Delete(GenFormDataContext context, Func<GenFormUser, Boolean> selector)
        {
            var dao = context.GenFormUser.Single(selector);
            context.GenFormUser.DeleteOnSubmit(dao);
        }

        public static Func<GenFormUser, Boolean> GetIdSelector(Int32 id)
        {
            return (user => user.UserId == id);
        }

        public static Func<GenFormUser, Boolean> CreateNameSelector(String name)
        {
            return (user => user.UserName == name);
        }
    }
}
