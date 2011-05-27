using System;
using System.Collections.Generic;

namespace Informedica.GenForm.Library.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetById(Int32 id);
        IEnumerable<T> GetByName(String name);
    }
}
