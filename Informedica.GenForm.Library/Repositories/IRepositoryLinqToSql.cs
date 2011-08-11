using System;
using System.Collections.Generic;

namespace Informedica.GenForm.Library.Repositories
{
    public interface IRepositoryLinqToSql<T>
    {
        IEnumerable<T> Fetch(Int32 id);
        IEnumerable<T> Fetch(String name);
        void Insert(T bo);
        void Delete(Int32 id);
        void Delete(T bo);
        IRollbackObject Rollback { get; }
    }

    public interface IRollbackObject: IDisposable
    {
    }
}
