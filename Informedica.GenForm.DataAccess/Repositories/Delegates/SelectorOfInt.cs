using System;

namespace Informedica.GenForm.DataAccess.Repositories.Delegates
{
    public delegate Func<T, Boolean> SelectorOfInt<T>(Int32 id);
}
