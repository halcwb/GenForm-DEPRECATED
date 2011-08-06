using Informedica.GenForm.Database;

namespace Informedica.GenForm.DataAccess.Repositories.Delegates
{
    public delegate void Insert<T>(GenFormDataContext context, T item);
}