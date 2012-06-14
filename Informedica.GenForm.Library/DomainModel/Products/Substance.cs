using System;
using System.Collections.Generic;
using Informedica.GenForm.DomainModel.Interfaces;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Validation;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Substance : Entity<Substance>, ISubstance
    {
        #region Private

        private readonly HashSet<Product> _products = new HashSet<Product>(new ProductComparer());

        #endregion

        #region Construction

        static Substance()
        {
            RegisterValidationRules();
        }

        protected Substance() {}

        #endregion

        #region Business

        internal protected virtual void ChangeName(string newName)
        {
            Name = newName;
        }

        public virtual void SetSubstanceGroup(ISubstanceGroup group)
        {
            if (SubstanceGroup == group) return;
            SubstanceGroup = group;
            group.AddSubstance(this);
        }

        public virtual ISubstanceGroup SubstanceGroup { get; protected internal set; }

        public virtual void RemoveFromSubstanceGroup()
        {
            if (SubstanceGroup == null) return;

            var group = SubstanceGroup;
            SubstanceGroup = null;
            ((SubstanceGroup)group).Remove(this);
        }

        public virtual void AddProduct(IProduct product)
        {
            if (_products.Contains((Product)product)) return;

            _products.Add((Product)product);
        }

        public virtual IEnumerable<IProduct> Products { get { return _products; } }

        internal protected virtual void AddProduct(Product product)
        {
            _products.Add(product);
        }

        #endregion

        #region Factory

        public static Substance Create(SubstanceDto dto)
        {
            var subst = new Substance
                       {
                           Name = dto.Name
                       };
            Validate(subst);
            return subst;
        }

        public static Substance Create(SubstanceDto dto, SubstanceGroup substanceGroup)
        {
            var substance = Create(dto);
            if (substanceGroup == null) return substance;

            substance.SetSubstanceGroup(substanceGroup);
            return substance;
        }

        #endregion

        #region Validation

        private static void RegisterValidationRules()
        {
            ValidationRulesManager.RegisterRule<Substance>(x => !String.IsNullOrWhiteSpace(x.Name));
        }

        #endregion

        #region Overrides of Entity<Substance,Guid>

        public override bool IsIdentical(Substance entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}