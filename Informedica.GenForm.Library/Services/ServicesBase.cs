using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel;
using Informedica.GenForm.Library.Factories;
using Informedica.GenForm.Library.Repositories;

namespace Informedica.GenForm.Library.Services
{
    public abstract class ServicesBase<TEnt, TId, TDto>
        where TEnt : Entity<TId, TDto>
        where TDto : DataTransferObject<TDto, TId>
    {
        private IRepository<TEnt, TId, TDto> _repository;
 
        protected EntityFactory<TEnt, TId, TDto> GetFactory(TDto dto)
        {
            return FactoryManager.Get<TEnt, TId, TDto>(dto);
        }

        protected TEnt GetById(TId id)
        {
            return Repository.SingleOrDefault(x => x.Id.Equals(id));
        }

        public IRepository<TEnt, TId, TDto> Repository
        {
            get { return _repository ?? (_repository = RepositoryFactory.Create<TEnt, TId, TDto>()); }
        }

        
    }
}
