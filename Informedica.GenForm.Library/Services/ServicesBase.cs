using System;
using Informedica.GenForm.Library.DomainModel;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.Factories;
using Informedica.GenForm.Library.Repositories;

namespace Informedica.GenForm.Library.Services
{
    public abstract class ServicesBase<TEnt, TDto>
        where TEnt : Entity<TEnt>
        where TDto : DataTransferObject<TDto>
    {
        private IRepository<TEnt> _repository;
 
        protected EntityFactory<TEnt, TDto> GetFactory(TDto dto) 
        {
            return FactoryManager.Get<TEnt, TDto>(dto);
        }

        protected TEnt GetById(Guid id)
        {
            return Repository.GetById(id);
        }

        protected TEnt GetByName(String name)
        {
            return Repository.GetByName(name);
        }

        public IRepository<TEnt> Repository
        {
            get { return _repository ?? (_repository = RepositoryFactory.Create<TEnt>()); }
        }

        
    }
}
