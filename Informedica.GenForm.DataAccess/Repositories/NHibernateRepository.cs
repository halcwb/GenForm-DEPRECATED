using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel;
using Informedica.GenForm.Library.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public abstract class NHibernateRepository<T, TId, TDto> : NHibernateBase, IRepository<T, TId, TDto>
        where T : Entity<TId, TDto>
        where TDto : DataTransferObject<TDto, TId>
    {
        protected NHibernateRepository(ISessionFactory factory) : base(factory) { }

        public IEnumerator<T> GetEnumerator()
        {
            return Transact(() => Session.Query<T>().GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Transact(() => GetEnumerator());
        }

        public abstract void Add(T item);

        public virtual void Add(T item, IEqualityComparer<T> comparer)
        {
            // Need to check
            // because item can be added by associated item
            if (this.Contains(item, comparer)) return; 
            Transact(() => Session.Save(item));
        }

        public virtual bool Contains(T item)
        {
            if (item.IdIsDefault(item.Id)) return false;
            return Transact(() => Session.Get<T>(item.Id)) != null;
        }

        public virtual int Count
        {
            // ToDo: This causes N+1 select problem
            get { return Transact(() => Session.Query<T>().Count()); }
        }

        public virtual bool Remove(T item)
        {
            // ToDo: Check tests whether this can be avoided
            // item can be removed by removal of associated item
            if (!Contains(item)) return true;
            Transact(() => Session.Delete(item));
            return true;
        }
    }
}
