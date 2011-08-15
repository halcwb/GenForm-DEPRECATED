using Informedica.GenForm.Library.DomainModel;
using Informedica.GenForm.Library.Repositories;
using StructureMap;

namespace Informedica.GenForm.Library.Factories
{
    public static class RepositoryFactory
    {
        public static IRepository<TEntity, TId, TDto> Create<TEntity, TId, TDto>() 
            where TEntity: Entity<TId, TDto>
            where TDto: DataTransferObject<TDto, TId>
        {
            return ObjectFactory.GetInstance<IRepository<TEntity, TId, TDto>>();
        }

    }
}
