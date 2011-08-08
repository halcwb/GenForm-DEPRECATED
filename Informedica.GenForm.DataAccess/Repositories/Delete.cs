using System;
using Informedica.GenForm.Database;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public delegate void Delete<T>(GenFormDataContext context, Func<T, bool> selector);
}
