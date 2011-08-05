using Informedica.GenForm.Database;

namespace Informedica.GenForm.DataAccess.Repositories.Delegates
{
    public delegate void InsertOnSubmit<T>(GenFormDataContext context, T item);
}