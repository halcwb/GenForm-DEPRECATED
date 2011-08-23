using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Relations;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class SubstanceGroup: Entity<Guid, SubstanceGroupDto>, ISubstanceGroup, IRelationPart
    {
        private ISubstanceGroup _mainSubstanceGroup;

        protected SubstanceGroup(): base(new SubstanceGroupDto()) {}

        private SubstanceGroup(SubstanceGroupDto dto): base(dto.CloneDto()) {}

        #region Implementation of ISubstanceGroup

        public virtual ISubstanceGroup MainSubstanceGroup
        {
            get { return _mainSubstanceGroup; }
            set { _mainSubstanceGroup = value; }
        }
        
        public virtual IEnumerable<Substance> Substances
        {
            get { return RelationProvider.SubstanceGroupSubstance.GetManyPart(this); }
            protected set { RelationProvider.SubstanceGroupSubstance.Add(this, new HashSet<Substance>(value)); }
        }

        #endregion

        public override bool IdIsDefault(Guid id)
        {
            return id == Guid.Empty;
        }

        public virtual void AddSubstance(Substance substance)
        {
            substance.AddToSubstanceGroup(this);
        }

        public virtual void Remove(Substance substance)
        {
            RelationProvider.SubstanceGroupSubstance.Remove(this, substance);
        }

        public static SubstanceGroup Create(SubstanceGroupDto dto)
        {
            return new SubstanceGroup(dto);
        }

        public virtual void ClearSubstances()
        {
            RelationProvider.SubstanceGroupSubstance.Clear(this);
        }
    }
}
