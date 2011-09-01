using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products.Collections;
using Informedica.GenForm.Library.DomainModel.Products.Interfaces;
using Informedica.GenForm.Library.DomainModel.Validation;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class SubstanceGroup: Entity<SubstanceGroup>
    {
        #region Private

        private SubstanceGroupDto _dto;
        private SubstanceSet _substances;

        #endregion

        #region Construction

        static SubstanceGroup()
        {
            RegisterValidationRules();
        }

        protected SubstanceGroup()
        {
            _dto = new SubstanceGroupDto();
            InitializeCollections();
        }

        private SubstanceGroup(SubstanceGroupDto dto)
        {
            ValidateDto(dto);
            InitializeCollections();
        }

        private void InitializeCollections()
        {
            _substances = new SubstanceSet(this);
        }

        #endregion

        #region Business

        public override Guid Id { get { return _dto.Id; } protected set { _dto.Id = value; } }

        public override string Name { get { return _dto.Name; } protected set { _dto.Name = value; } }

        public virtual SubstanceGroup MainSubstanceGroup { get; set; }

        public virtual IEnumerable<ISubstance> Substances
        {
            get { return _substances;  }
        }
        
        public virtual Iesi.Collections.Generic.ISet<Substance> SubstanceSet
        {
            get { return _substances.GetEntitySet(); }
            protected set { _substances = new SubstanceSet(value, this); }
        }

        public virtual bool ContainsSubstance(ISubstance subst)
        {
            return _substances.Contains(subst);
        }

        public virtual void AddSubstance(ISubstance substance)
        {
            _substances.Add(substance);
        }

        public virtual void Remove(ISubstance substance)
        {
            _substances.Remove(substance);
        }

        public virtual void ClearAllSubstances()
        {
            var list = new List<Substance>(SubstanceSet);
            foreach (var substance in list)
            {
                substance.RemoveFromSubstanceGroup();
            }
        }

        #endregion

        #region Factory

        public static SubstanceGroup Create(SubstanceGroupDto dto)
        {
            return new SubstanceGroup(dto);
        }

        #endregion

        #region Validation

        private static void RegisterValidationRules()
        {
            ValidationRulesManager.RegisterRule<SubstanceGroupDto>(x => !String.IsNullOrWhiteSpace(x.Name));
        }

        protected override void SetDto<TDto>(TDto dto)
        {
            var dataTransferObject = dto as DataTransferObject<SubstanceGroupDto>;
            if (dataTransferObject != null) _dto = dataTransferObject.CloneDto();
        }

        #endregion
    }
}
