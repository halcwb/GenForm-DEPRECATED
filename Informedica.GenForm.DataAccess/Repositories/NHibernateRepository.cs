﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel;
using Informedica.GenForm.Library.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public abstract class NHibernateRepository<TEnt> : NHibernateBase, IRepository<TEnt>
        where TEnt : Entity<TEnt>
    {
        protected NHibernateRepository(ISessionFactory factory) : base(factory) { }

        public IEnumerator<TEnt> GetEnumerator()
        {
            return Transact(() => Session.Query<TEnt>().GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Transact(() => GetEnumerator());
        }

        public abstract void Add(TEnt item);

        public virtual void Add(TEnt item, IEqualityComparer<TEnt> comparer)
        {
            // Need to check
            // because item can be added by associated item
            if (this.Contains(item, comparer)) return; 
            Transact(() => Session.Save(item));
        }

        public virtual bool Contains(TEnt item)
        {
            if (item.IsTransient()) return false;
            return Transact(() => Session.Get<TEnt>(item.Id)) != null;
        }

        public virtual int Count
        {
            // ToDo: This causes N+1 select problem
            get { return Transact(() => Session.Query<TEnt>().Count()); }
        }

        public virtual bool Remove(TEnt item)
        {
            // ToDo: Check tests whether this can be avoided
            // item can be removed by removal of associated item
            if (!Contains(item)) return true;
            Transact(() => Session.Delete(item));
            return true;
        }

        public void Flush()
        {
            Session.Flush();
        }

        public void Clear()
        {
            Session.Clear();
        }
    }
}
