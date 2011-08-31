using System;
using Informedica.GenForm.Library.Repositories;

namespace Informedica.GenForm.Library.DomainModel.Equality
{
    public static class IdentityChanger
    {
        public static void ChangeIdentity<TEnt, TProp>(Action<TProp> changeIdentity, TProp newIdentity, IRepository<TEnt> repository) 
            where TEnt : Entity<TEnt>
        {
            changeIdentity.Invoke(newIdentity);
            repository.Flush();
            repository.Clear();
        }
    }
}
