using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel;

namespace Informedica.GenForm.Library.Repositories
{
    public interface IRepository<TEnt>: IEnumerable<TEnt> 
        where TEnt : Entity<TEnt>
    {
        void Add(TEnt item);
        bool Contains(TEnt item);
        int Count { get; }
        bool Remove(TEnt item);
        void Flush();
        void Clear();
    }

}
