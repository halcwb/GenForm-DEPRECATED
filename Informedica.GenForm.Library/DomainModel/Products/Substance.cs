using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products.Interfaces;
using Informedica.GenForm.Library.DomainModel.Validation;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Substance : Entity<Substance>, ISubstance
    {
        #region Private

        private readonly HashSet<Product> _products = new HashSet<Product>(new ProductComparer());
        private SubstanceDto _dto;

        #endregion

        #region Construction

        static Substance()
        {
            RegisterValidationRules();
        }

        protected Substance()
        {
            _dto = new SubstanceDto();
        }

        private Substance(SubstanceDto dto)
        {
            ValidateDto(dto);

            if (_dto.SubstanceGroupName == null) return;

            var group = SubstanceGroup.Create(new SubstanceGroupDto
                                            {
                                                Id = _dto.SubstanceGroupId,
                                                Name = _dto.SubstanceGroupName
                                            });
            group.AddSubstance(this);
        }

        #endregion

        #region Business

        public override Guid Id { get { return _dto.Id; } protected set { _dto.Id = value; } }

        public override string Name { get { return _dto.Name; } protected set { _dto.Name = value; } }

        internal protected virtual void ChangeName(string newName)
        {
            Name = newName;
        }

        public virtual void SetSubstanceGroup(SubstanceGroup group)
        {
            if (SubstanceGroup == group) return;
            SubstanceGroup = group;
            group.AddSubstance(this);
        }

        public virtual SubstanceGroup SubstanceGroup { get; protected internal set; }

        public virtual void RemoveFromSubstanceGroup()
        {
            if (SubstanceGroup == null) return;

            var group = SubstanceGroup;
            SubstanceGroup = null;
            group.Remove(this);
        }

        public virtual IEnumerable<Product> Products { get { return _products; } }

        internal protected virtual void AddProduct(Product product)
        {
            _products.Add(product);
        }

        #endregion

        #region Factory

        public static Substance Create(SubstanceDto dto)
        {
            return new Substance(dto);
        }

        public static Substance Create(SubstanceDto dto, SubstanceGroup substanceGroup)
        {
            var substance = new Substance(dto);
            if (substanceGroup == null) return substance;
            substance.SetSubstanceGroup(substanceGroup);
            return substance;
        }

        #endregion

        #region Validation

        private static void RegisterValidationRules()
        {
            ValidationRulesManager.RegisterRule<SubstanceDto>(x => !String.IsNullOrWhiteSpace(x.Name));
        }

        protected override void SetDto<TDto>(TDto dto)
        {
            var dataTransferObject = dto as DataTransferObject<SubstanceDto>;
            if (dataTransferObject != null) _dto = dataTransferObject.CloneDto();
        }

        #endregion
    }
}