using System;
using System.Collections.Generic;
using System.Linq;
using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Validation;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class SubstanceGroup: Entity<SubstanceGroup>
    {
        private SubstanceGroup _mainSubstanceGroup;
        private Iesi.Collections.Generic.ISet<Substance> _substances = new HashedSet<Substance>();
        private readonly IEqualityComparer<Substance> _substanceComparer = new SubstanceComparer();
        private SubstanceGroupDto _dto;

        static SubstanceGroup()
        {
            RegisterValidationRules();
        }

        protected SubstanceGroup(): base()
        {
            _dto = new SubstanceGroupDto();
        }

        private SubstanceGroup(SubstanceGroupDto dto): base()
        {
            ValidateDto(dto);
        }

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


        public override Guid Id { get { return _dto.Id; } protected set { _dto.Id = value; } }

        public override string Name { get { return _dto.Name; } protected set { _dto.Name = value; } }

        private static void RegisterValidationRules()
        {
            ValidationRulesManager.RegisterRule<SubstanceGroupDto>(x => !String.IsNullOrWhiteSpace(x.Name));
        }

        protected override void SetDto<TDto>(TDto dto)
        {
            var dataTransferObject = dto as DataTransferObject<SubstanceGroupDto>;
            if (dataTransferObject != null) _dto = dataTransferObject.CloneDto();
        }
    }
}
