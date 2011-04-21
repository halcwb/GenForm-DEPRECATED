using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Informedica.GenForm.Library.DomainModel.Users;

namespace Informedica.GenForm.Library.DataAccess
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetById(Int32 id);
        IEnumerable<T> GetByName(String name);
    }
}
