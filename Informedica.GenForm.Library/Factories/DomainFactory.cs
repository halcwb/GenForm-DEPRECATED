using System;
using System.Linq;
using Informedica.Factory;
using Informedica.GenForm.Library.DomainModel;
using Informedica.GenForm.Library.Repositories;

namespace Informedica.GenForm.Library.Factories
{
    public static class DomainFactory
    {
        public static T Create<T, TC>(TC dto)
        {
            return ObjectFactory.Instance.With(dto).GetInstance<T>();
        }

        public static T CreateOrGetById<T, TC>(TC dto)
            where T : Entity<Guid, TC>
            where TC : DataTransferObject<TC, Guid>
        {
            var repository = ObjectFactory.Instance.GetInstance<IRepository<T, Guid, TC>>();

            T entity = repository.SingleOrDefault(x => x.Id == dto.Id);
            if (entity == null)
            {
                entity = ObjectFactory.Instance.With(dto).GetInstance<T>();
                repository.Add(entity);
            }
            return entity;
        }
    }
}