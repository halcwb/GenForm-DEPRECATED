using System;
using System.Collections.Generic;
using System.Linq;
using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class SubstanceGroup: Entity<Guid, SubstanceGroupDto>
    {
        private SubstanceGroup _mainSubstanceGroup;
        private Iesi.Collections.Generic.ISet<Substance> _substances = new HashedSet<Substance>();
        private readonly IEqualityComparer<Substance> _substanceComparer = new SubstanceComparer();

        protected SubstanceGroup(): base(new SubstanceGroupDto()) {}

        private SubstanceGroup(SubstanceGroupDto dto): base(dto.CloneDto()) {}

        #region Implementation of ISubstanceGroup

        public virtual SubstanceGroup MainSubstanceGroup
        {
            get { return _mainSubstanceGroup; }
            set { _mainSubstanceGroup = value; }
        }
        
        public virtual Iesi.Collections.Generic.ISet<Substance> Substances
        {
            get { return _substances; }
            protected set { _substances = value; }
        }

        #endregion

        public override bool IdIsDefault(Guid id)
        {
            return id == Guid.Empty;
        }

        public virtual void AddSubstance(Substance substance)
        {
            if (_substances.Contains(substance)) return;

            _substances.Add(substance);
            substance.SubstanceGroup = this;
        }

        public virtual void Remove(Substance substance)
        {
            if (!_substances.Contains(substance)) return;
            
            _substances.Remove(substance);
            substance.SubstanceGroup = null;
        }

        public static SubstanceGroup Create(SubstanceGroupDto dto)
        {
            return new SubstanceGroup(dto);
        }

        public virtual void ClearAllSubstances()
        {
            var list = new List<Substance>(Substances);
            foreach (var substance in list)
            {
                substance.RemoveFromSubstanceGroup();
            }
        }

        public virtual bool ContainsSubstance(Substance subst)
        {
            return _substances.Contains(subst, _substanceComparer);
        }
    }
}
