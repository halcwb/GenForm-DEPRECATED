using System;
using System.Collections.Generic;
using Informedica.GenForm.Database;

namespace Informedica.GenForm.DataAccess.Repositories.Delegates
{
    public delegate IEnumerable<T> Fetch<T>(GenFormDataContext context, Func<T, Boolean> selector);

}
