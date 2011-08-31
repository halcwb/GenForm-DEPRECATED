using Informedica.GenForm.Library.DomainModel;
using Informedica.GenForm.Library.Repositories;
using StructureMap;

namespace Informedica.GenForm.Library.Factories
{
    public static class RepositoryFactory
    {
        public static IRepository<TEnt> Create<TEnt>() where TEnt : Entity<TEnt>
        {
            return ObjectFactory.GetInstance<IRepository<TEnt>>();
        }

    }
}
