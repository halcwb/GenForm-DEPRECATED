using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Informedica.GenForm.Library.DataAccess
{
    public interface IRepository<T>
    {
        T GetById(Int32 id);
        T GetByName(String name);
    }
}
