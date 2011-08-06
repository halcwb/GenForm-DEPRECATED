using System;
using System.Collections.Generic;

namespace Informedica.GenForm.Library.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> Fetch(Int32 id);
        IEnumerable<T> Fetch(String name);
        void Insert(T item);
        void Delete(Int32 id);
        void Delete(T item);
        IRollbackObject Rollback { get; }
    }

    public interface IRollbackObject: IDisposable
    {
    }
}
