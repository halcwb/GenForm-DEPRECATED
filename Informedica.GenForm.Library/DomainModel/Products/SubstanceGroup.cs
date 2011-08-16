using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.Exceptions;
using StructureMap;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class SubstanceGroup: Entity<Guid, SubstanceGroupDto>, ISubstanceGroup
    {
        private ISubstanceGroup _mainSubstanceGroup;
        private ISet<Substance> _substances = new HashSet<Substance>(new SubstanceComparer());

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
            get { return _substances; }
            protected set { _substances = new HashSet<Substance>(value, new SubstanceComparer()); }
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

        internal protected virtual void AddSubstance(Substance substance, Action<SubstanceGroup> setSubstanceGroup)
        {
            if (_substances.Contains(substance, new SubstanceComparer()))
                throw new CannotAddItemException<Substance>(substance);
            _substances.Add(substance);
            setSubstanceGroup(this);
        }

        public virtual void Remove(Substance substance)
        {
            _substances.Remove(substance);
        }

        public static SubstanceGroup Create(SubstanceGroupDto dto)
        {
            return new SubstanceGroup(dto);
        }
    }
}
