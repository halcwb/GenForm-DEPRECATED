using System;
using Informedica.GenForm.Database;

namespace Informedica.GenForm.DataAccess.Repositories.Delegates
{
    public delegate void Delete<T>(GenFormDataContext context, Func<T, Boolean> selector);
}
