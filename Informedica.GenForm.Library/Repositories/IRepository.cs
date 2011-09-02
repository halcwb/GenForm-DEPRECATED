using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel;

namespace Informedica.GenForm.Library.Repositories
{
    public interface IRepository<TEnt>: IEnumerable<TEnt> 
        where TEnt : Entity<TEnt>
    {
        TEnt GetById(Guid id);
        TEnt GetByName(string name);

        bool Contains(TEnt entity);
        void Add(TEnt entity);
        bool Remove(TEnt entity);
        int Count { get; }

        void Flush();
        void Clear();
    }

}
