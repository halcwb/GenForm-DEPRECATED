using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products.Collections;
using Informedica.GenForm.Library.DomainModel.Products.Interfaces;
using Informedica.GenForm.Library.DomainModel.Validation;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class SubstanceGroup: Entity<SubstanceGroup>, ISubstanceGroup
    {
        #region Private

        private SubstanceSet _substances;

        #endregion

        #region Construction

        static SubstanceGroup()
        {
            RegisterValidationRules();
        }

        protected SubstanceGroup()
        {
            InitializeCollections();
        }

        private void InitializeCollections()
        {
            _substances = new SubstanceSet(this);
        }

        #endregion

        #region Business

        public virtual ISubstanceGroup MainSubstanceGroup { get; set; }

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
            var group = new SubstanceGroup
                       {
                           Name = dto.Name,
                       };
            Validate(group);
            return group;
        }

        #endregion

        #region Validation

        private static void RegisterValidationRules()
        {
            ValidationRulesManager.RegisterRule<SubstanceGroup>(x => !String.IsNullOrWhiteSpace(x.Name));
        }

        #endregion
    }
}
