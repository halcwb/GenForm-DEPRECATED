using System;
using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class SubstanceGroup: Entity<Guid, SubstanceGroupDto>
    {
        private SubstanceGroup _mainSubstanceGroup;
        private ISet<Substance> _substances = new HashedSet<Substance>();

        protected SubstanceGroup(): base(new SubstanceGroupDto()) {}

        private SubstanceGroup(SubstanceGroupDto dto): base(dto.CloneDto()) {}

        #region Implementation of ISubstanceGroup

        public virtual SubstanceGroup MainSubstanceGroup
        {
            get { return _mainSubstanceGroup; }
            set { _mainSubstanceGroup = value; }
        }
        
        public virtual ISet<Substance> Substances
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
            if (_substances.Contains(substance))
            {
                _substances.Remove(substance);
                substance.SubstanceGroup = null;
            }
        }

        public static SubstanceGroup Create(SubstanceGroupDto dto)
        {
            return new SubstanceGroup(dto);
        }

        public virtual void ClearSubstances()
        {
            foreach (var substance in Substances)
            {
                substance.RemoveFromSubstanceGroup();
            }
        }
    }
}
