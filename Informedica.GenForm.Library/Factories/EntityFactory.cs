using System.Linq;
using Informedica.GenForm.Library.DomainModel;
using Informedica.GenForm.Library.Repositories;

namespace Informedica.GenForm.Library.Factories
{
    public abstract class EntityFactory<TEnt, TId, TDto>
        where TEnt : Entity<TId,TDto>
        where TDto : DataTransferObject<TDto, TId>
    {
        protected TDto Dto;
        private IRepository<TEnt, TId, TDto> _repository;

        protected EntityFactory(TDto dto)
        {
            Dto = dto;
        } 

        private IRepository<TEnt, TId, TDto> Repository
        {
            get { return _repository ?? (_repository = RepositoryFactory.Create<TEnt, TId, TDto>()); }
        }

        public TEnt Get()
        {
            return Find() ?? Create();
        }

        protected TEnt Find()
        {
            return Repository.SingleOrDefault(x => x.Name == Dto.Name);
        }

        protected abstract TEnt Create();

        protected void Add(TEnt entity)
        {
            // Performance improvement return if entity already 
            // added by an associated entity
            if (!entity.IdIsDefault(entity.Id)) return;
            Repository.Add(entity);
        }
    }
}
