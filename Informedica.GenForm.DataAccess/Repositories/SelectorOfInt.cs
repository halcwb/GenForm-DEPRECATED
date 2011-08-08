using System;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public delegate Func<T, bool> SelectorOfInt<T>(Int32 id);
}
