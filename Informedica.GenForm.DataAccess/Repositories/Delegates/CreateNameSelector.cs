using System;

namespace Informedica.GenForm.DataAccess.Repositories.Delegates
{
    public delegate Func<T, bool> CreateNameSelector<T>(String name);
}