using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.Exceptions;

namespace Informedica.GenForm.Library.DomainModel.Products.Collections
{
    internal abstract class EntityCollection<TEnt, TParent> : IEnumerable<TEnt>
    {
        private readonly Iesi.Collections.Generic.ISet<TEnt> _set;
        private readonly TParent _parent;
        private readonly IEqualityComparer<TEnt> _comparer;

        protected EntityCollection(Iesi.Collections.Generic.ISet<TEnt> set, TParent parent, IEqualityComparer<TEnt> comparer)
        {
            _set = set;
            _parent = parent;
            _comparer = comparer;
        } 

        public IEnumerator<TEnt> GetEnumerator()
        {
            return _set.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Contains(TEnt entity)
        {
            return _set.Any(x => _comparer.Equals(x, entity));
        }

        internal Iesi.Collections.Generic.ISet<TEnt> GetEntitySet()
        {
            return _set;
        }

        internal protected virtual void Add(TEnt entity, Action<TParent> addParent)
        {
            if (_set.Contains(entity)) return;

            if (Contains(entity)) throw new CannotAddItemException<TEnt>(entity);

            _set.Add(entity);
            addParent.Invoke(_parent);
        }

        internal protected virtual void Remove(TEnt entity, Action<TParent> removeParent)
        {
            if (_set.Contains(entity)) return;

            _set.Remove(entity);
            removeParent.Invoke(_parent);
        }
    }
}
