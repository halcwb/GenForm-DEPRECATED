using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel;

namespace Informedica.GenForm.Library.Repositories
{
    public interface IRepository<T, TId, TDto>: IEnumerable<T> 
        where T: Entity<TId, TDto>
        where TDto: DataTransferObject<TDto, TId>
    {
        void Add(T item);
        bool Contains(T item);
        int Count { get; }
        bool Remove(T item);
    }

}
