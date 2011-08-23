using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel;

namespace Informedica.GenForm.Library.DomainModel.Relations
{
    public class ManyToManyRelation<TLeftPart, TRightPart> : IRelation<IRelationPart, IRelationPart>
        where TLeftPart : class, IRelationPart
        where TRightPart : class, IRelationPart
    {
        private readonly IDictionary<TLeftPart, ISet<TRightPart>> _oneToManyRight;
        private readonly IDictionary<TRightPart, ISet<TLeftPart>> _oneToManyLeft;

        public ManyToManyRelation()
        {
            _oneToManyLeft = new ConcurrentDictionary<TRightPart, ISet<TLeftPart>>();
            _oneToManyRight = new ConcurrentDictionary<TLeftPart, ISet<TRightPart>>();
        } 

        public IEnumerable<TLeftPart> GetManyPartLeft(TRightPart rightPart)
        {
            return _oneToManyLeft.ContainsKey(rightPart) ? _oneToManyLeft[rightPart] : new HashSet<TLeftPart>();
        }

        public void Add(TLeftPart leftPart, TRightPart rightPart)
        {
            if (leftPart == null || rightPart == null) return;

            if(!_oneToManyLeft.ContainsKey(rightPart)) _oneToManyLeft.Add(rightPart, new HashSet<TLeftPart>());
            _oneToManyLeft[rightPart].Add(leftPart);
            if(!_oneToManyRight.ContainsKey(leftPart)) _oneToManyRight.Add(leftPart, new HashSet<TRightPart>());
            _oneToManyRight[leftPart].Add(rightPart);
        }

        public IEnumerable<TRightPart> GetManyPartRight(TLeftPart leftPart)
        {
            return _oneToManyRight.ContainsKey(leftPart) ? _oneToManyRight[leftPart] : new HashSet<TRightPart>();
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

        public void Add(TLeftPart leftPart, IEnumerable<TRightPart> rightParts)
        {
            if (leftPart == null || rightParts == null || rightParts.Count() == 0) return;

            if (!_oneToManyRight.ContainsKey(leftPart)) _oneToManyRight.Add(leftPart, new HashSet<TRightPart>());
            foreach (var rightPart in rightParts)
            {
                _oneToManyRight[leftPart].Add(rightPart);
                if (!_oneToManyLeft.ContainsKey(rightPart)) _oneToManyLeft.Add(rightPart, new HashSet<TLeftPart>());
                _oneToManyLeft[rightPart].Add(leftPart);
            }
        }

        public void Add(IEnumerable<TLeftPart> leftParts, TRightPart rightPart)
        {
            if (leftParts == null || leftParts.Count() == 0 || rightPart == null) return;

            if (!_oneToManyLeft.ContainsKey(rightPart)) _oneToManyLeft.Add(rightPart, new HashSet<TLeftPart>());
            foreach (var leftPart in leftParts)
            {
                _oneToManyLeft[rightPart].Add(leftPart);
                if (!_oneToManyRight.ContainsKey(leftPart)) _oneToManyRight.Add(leftPart, new HashSet<TRightPart>());
                _oneToManyRight[leftPart].Add(rightPart);
            }
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
            var leftList = new HashSet<TLeftPart>(_oneToManyLeft[rightPart]);
            foreach (var leftPart in leftList)
            {
                _oneToManyRight[leftPart].Remove(rightPart);
            }
            _oneToManyLeft.Remove(rightPart);
        }
    }
}