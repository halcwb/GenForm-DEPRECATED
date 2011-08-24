using System.Collections.Concurrent;
using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace Informedica.GenForm.Library.DomainModel.Relations
{
    public class OneToManyRelation<TOnePart, TManyPart> : IRelation<IRelationPart, IRelationPart>
        where TOnePart : class, IRelationPart
        where TManyPart : class, IRelationPart
    {
        private readonly IDictionary<TOnePart, Iesi.Collections.Generic.ISet<TManyPart>> _oneToMany;
        private readonly IDictionary<TManyPart, TOnePart> _manyToOne;

        public OneToManyRelation()
        {
            _manyToOne = new ConcurrentDictionary<TManyPart, TOnePart>();
            _oneToMany = new ConcurrentDictionary<TOnePart, Iesi.Collections.Generic.ISet<TManyPart>>();
        }

        public void Set(TOnePart onePart, TManyPart manyPart)
        {
            if (onePart == null || manyPart == null) return;
            if (!_manyToOne.ContainsKey(manyPart)) _manyToOne.Add(manyPart, onePart);
            else _manyToOne[manyPart] = onePart;
        }

        public void Add(TOnePart onePart, TManyPart manyPart)
        {
            if (onePart == null || manyPart == null) return;

            if (!_manyToOne.ContainsKey(manyPart)) _manyToOne.Add(manyPart, onePart);
            else _manyToOne[manyPart] = onePart;
            if (!_oneToMany.ContainsKey(onePart)) _oneToMany.Add(onePart, new HashedSet<TManyPart>());
            _oneToMany[onePart].Add(manyPart);
        }

        public Iesi.Collections.Generic.ISet<TManyPart> GetManyPart(TOnePart onePart)
        {
            if (!_oneToMany.ContainsKey(onePart)) _oneToMany[onePart] = new HashedSet<TManyPart>();
            return _oneToMany[onePart];
        }

        public TOnePart GetOnePart(TManyPart manyPart)
        {
            if(!_manyToOne.ContainsKey(manyPart)) _manyToOne[manyPart] = default(TOnePart);
            return _manyToOne[manyPart];
        }

        public void Remove(TOnePart onePart, TManyPart manyPart)
        {
            _manyToOne.Remove(manyPart);
            _oneToMany[onePart].Remove(manyPart);
        }

        public void Set(TOnePart onePart, Iesi.Collections.Generic.ISet<TManyPart> manyPart)
        {
            if (onePart == null) return;
            if (!_oneToMany.ContainsKey(onePart)) _oneToMany.Add(onePart, manyPart);
            else _oneToMany[onePart] = manyPart;
        }

        public void Clear(TOnePart onePart)
        {
            var list = new HashedSet<TManyPart>(_oneToMany[onePart]);
            foreach (var manyPart in list)
            {
                _manyToOne.Remove(manyPart);
            }
            _oneToMany.Remove(onePart);
        }

        public void Clear(TManyPart manyPart)
        {
            if (!_manyToOne.ContainsKey(manyPart)) return;
            if (_manyToOne[manyPart] == null) return;

            _oneToMany[_manyToOne[manyPart]].Remove(manyPart);
            _manyToOne.Remove(manyPart);
        }
    }
}