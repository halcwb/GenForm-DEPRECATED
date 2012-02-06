using System;
using Informedica.GenForm.Library.DomainModel;

namespace Informedica.GenForm.Library.Repositories
{
    public interface IRepository<TEnt>: EntityRepository.IRepository<TEnt, Guid> where TEnt : Entity<TEnt>
    {
        TEnt GetByName(string name);
    }

}
