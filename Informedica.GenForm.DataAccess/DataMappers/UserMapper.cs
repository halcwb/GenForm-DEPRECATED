using System;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Users;

namespace Informedica.GenForm.DataAccess.DataMappers
{
    public class UserMapper: IDataMapper<IUser, GenFormUser>
    {
        #region Implementation of IDataMapper<IUser,IUserDao>

        public void MapFromBoToDao(IUser bo, GenFormUser dao)
        {
            throw new NotImplementedException();
        }

        public void MapFromDaoToBo(GenFormUser dao, IUser bo)
        {
            bo.FirstName = dao.FirstName;
            bo.LastName = dao.LastName;
            bo.Pager = dao.PagerNumber;
            bo.Password = dao.PassWord;
            bo.UserName = dao.UserName;
        }

        #endregion
    }
}