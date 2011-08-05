using System;

namespace Informedica.GenForm.DataAccess.Repositories.Delegates
{
    public delegate Func<T, Boolean> CreateIdSelector<T>(Int32 id);
}
