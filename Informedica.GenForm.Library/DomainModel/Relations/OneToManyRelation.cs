using System.Collections.Concurrent;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.DomainModel.Relations
{
    public class OneToManyRelation<TOnePart, TManyPart> : IRelation<IRelationPart, IRelationPart>
        where TOnePart : class, IRelationPart
        where TManyPart : class, IRelationPart
    {
        private readonly IDictionary<TOnePart, ISet<TManyPart>> _oneToMany;
        private readonly IDictionary<TManyPart, TOnePart> _manyToOne;

        public OneToManyRelation()
        {
            _manyToOne = new ConcurrentDictionary<TManyPart, TOnePart>();
            _oneToMany = new ConcurrentDictionary<TOnePart, ISet<TManyPart>>();
        }

        public void Add(TOnePart onePart, TManyPart manyPart)
        {
            if (onePart == null || manyPart == null) return;

            _manyToOne.Add(manyPart, onePart);
            if (!_oneToMany.ContainsKey(onePart)) _oneToMany.Add(onePart, new HashSet<TManyPart>());
            _oneToMany[onePart].Add(manyPart);
        }

        public IEnumerable<TManyPart> GetManyPart(TOnePart onePart)
        {
            return _oneToMany.ContainsKey(onePart) ? _oneToMany[onePart] : new HashSet<TManyPart>();
        }

        public TOnePart GetOnePart(TManyPart manyPart)
        {
            return _manyToOne.ContainsKey(manyPart) ? _manyToOne[manyPart] : default(TOnePart);
        }

        public void Remove(TOnePart onePart, TManyPart manyPart)
        {
            _manyToOne.Remove(manyPart);
            _oneToMany[onePart].Remove(manyPart);
        }

        public void Add(TOnePart onePart, HashSet<TManyPart> manyPart)
        {
            if (onePart == null || manyPart.Count == 0) return;

            if (!_oneToMany.ContainsKey(onePart)) _oneToMany.Add(onePart, new HashSet<TManyPart>());

            foreach (var part in manyPart)
            {
                if (_manyToOne.ContainsKey(part)) continue;
       
                _manyToOne.Add(part, onePart);
                _oneToMany[onePart].Add(part);
            }
        }

        public void Clear(TOnePart onePart)
        {
            var list = new HashSet<TManyPart>(_oneToMany[onePart]);
            foreach (var manyPart in list)
            {
                _manyToOne.Remove(manyPart);
            }
            _oneToMany.Remove(onePart);
        }

        public void Clear(TManyPart manyPart)
        {
            if (!_manyToOne.ContainsKey(manyPart)) return;
            
            _oneToMany[_manyToOne[manyPart]].Remove(manyPart);
            _manyToOne.Remove(manyPart);
        }
    }
}