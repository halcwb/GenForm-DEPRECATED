using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel;
using Informedica.GenForm.Library.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class NHibernateRepository<T, TId, TDto> : NHibernateBase, IRepository<T, TId, TDto>
        where T : Entity<TId, TDto>
        where TDto : DataTransferObject<TDto, TId>
    {
        public NHibernateRepository(ISessionFactory factory) : base(factory) { }

        public IEnumerator<T> GetEnumerator()
        {
            return Transact(() => Session.Query<T>().GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Transact(() => GetEnumerator());
        }

        public virtual void Add(T item)
        {
            Transact(() => Session.Save(item));
        }

        public virtual void Add(T item, IEqualityComparer<T> comparer)
        {
            if (this.Contains(item, comparer)) throw new NonUniqueObjectException(item.Id, item.ToString());
            // ToDo: temp hack to avoid loop through Add of derived class
            Transact(() => Session.Save(item));
        }

        public virtual bool Contains(T item)
        {
            if (item.IdIsDefault(item.Id)) return false;
            return Transact(() => Session.Get<T>(item.Id)) != null;
        }

        public virtual int Count
        {
            get { return Transact(() => Session.Query<T>().Count()); }
        }

        public virtual bool Remove(T item)
        {
            Transact(() => Session.Delete(item));
            return true;
        }
    }
}
