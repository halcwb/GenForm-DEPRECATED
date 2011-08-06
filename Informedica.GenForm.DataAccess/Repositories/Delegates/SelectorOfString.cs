using System;

namespace Informedica.GenForm.DataAccess.Repositories.Delegates
{
    public delegate Func<T, Boolean> SelectorOfString<T>(String name);
}