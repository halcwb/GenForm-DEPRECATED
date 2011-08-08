using System;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public delegate Func<T, bool> SelectorOfString<T>(String name);
}