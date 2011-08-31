using System.Linq;
using Informedica.GenForm.Library.DomainModel;
using Informedica.GenForm.Library.Repositories;

namespace Informedica.GenForm.Library.Factories
{
    public abstract class EntityFactory<TEnt, TDto>
        where TEnt : Entity<TEnt>
        where TDto : DataTransferObject<TDto>
    {
        protected TDto Dto;
        private IRepository<TEnt> _repository;

        protected EntityFactory(TDto dto)
        {
            Dto = dto;
        } 

        private IRepository<TEnt> Repository
        {
            get { return _repository ?? (_repository = RepositoryFactory.Create<TEnt>()); }
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
            if (!entity.IsTransient()) return;
            Repository.Add(entity);
        }
    }
}
