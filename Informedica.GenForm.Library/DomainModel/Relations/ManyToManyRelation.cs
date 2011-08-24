using System.Collections.Concurrent;
using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace Informedica.GenForm.Library.DomainModel.Relations
{
    public class ManyToManyRelation<TLeftPart, TRightPart> : IRelation<IRelationPart, IRelationPart>
        where TLeftPart : class, IRelationPart
        where TRightPart : class, IRelationPart
    {
        private readonly IDictionary<TLeftPart, Iesi.Collections.Generic.ISet<TRightPart>> _oneToManyRight;
        private readonly IDictionary<TRightPart, Iesi.Collections.Generic.ISet<TLeftPart>> _oneToManyLeft;

        public ManyToManyRelation()
        {
            _oneToManyLeft = new ConcurrentDictionary<TRightPart, Iesi.Collections.Generic.ISet<TLeftPart>>();
            _oneToManyRight = new ConcurrentDictionary<TLeftPart, Iesi.Collections.Generic.ISet<TRightPart>>();
        } 

        public Iesi.Collections.Generic.ISet<TLeftPart> GetManyPartLeft(TRightPart rightPart)
        {
            if (!_oneToManyLeft.ContainsKey(rightPart))  _oneToManyLeft[rightPart] = new HashedSet<TLeftPart>();
            return _oneToManyLeft[rightPart];
        }

        public void Add(TLeftPart leftPart, TRightPart rightPart)
        {
            if (leftPart == null || rightPart == null) return;

            if(!_oneToManyLeft.ContainsKey(rightPart)) _oneToManyLeft.Add(rightPart, new HashedSet<TLeftPart>());
            _oneToManyLeft[rightPart].Add(leftPart);
            if(!_oneToManyRight.ContainsKey(leftPart)) _oneToManyRight.Add(leftPart, new HashedSet<TRightPart>());
            _oneToManyRight[leftPart].Add(rightPart);
        }

        public Iesi.Collections.Generic.ISet<TRightPart> GetManyPartRight(TLeftPart leftPart)
        {
            if (!_oneToManyRight.ContainsKey(leftPart)) _oneToManyRight[leftPart] = new HashedSet<TRightPart>();
            return _oneToManyRight[leftPart];
        }

        public void Remove(TLeftPart leftPart, TRightPart rightPart)
        {
            var rightList = new HashSet<TRightPart>(_oneToManyRight[leftPart]);
            foreach (var part in rightList)
            {
                if (part.Equals(rightPart)) _oneToManyRight[leftPart].Remove(part); 
            }
            var leftList = new HashSet<TLeftPart>(_oneToManyLeft[rightPart]);
            foreach (var part in leftList)
            {
                if (part.Equals(leftPart)) _oneToManyLeft[rightPart].Remove(part); 
            }
        }

        public void Set(TLeftPart leftPart, Iesi.Collections.Generic.ISet<TRightPart> rightParts)
        {
            if (leftPart == null) return;

            if (!_oneToManyRight.ContainsKey(leftPart)) _oneToManyRight.Add(leftPart, null);
            _oneToManyRight[leftPart] = rightParts;
        }

        public void Set(Iesi.Collections.Generic.ISet<TLeftPart> leftParts, TRightPart rightPart)
        {
            if (rightPart == null) return;

            if (!_oneToManyLeft.ContainsKey(rightPart)) _oneToManyLeft.Add(rightPart, null);
            _oneToManyLeft[rightPart] = leftParts;
        }

        public void Clear(TLeftPart leftPart)
        {
            var rightList = new HashSet<TRightPart>(_oneToManyRight[leftPart]);
            foreach (var rightPart in rightList)
            {
                _oneToManyLeft[rightPart].Remove(leftPart);
            }
            _oneToManyRight.Remove(leftPart);
        }

        public void Clear(TRightPart rightPart)
        {
            var leftList = new HashedSet<TLeftPart>(_oneToManyLeft[rightPart]);
            foreach (var leftPart in leftList)
            {
                _oneToManyRight[leftPart].Remove(rightPart);
            }
            _oneToManyLeft.Remove(rightPart);
        }
    }
}