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
        private readonly HashSet<Product> _products = new HashSet<Product>(new ProductComparer());
        private SubstanceGroup _substanceGroup;
        private SubstanceDto _dto;

        static Substance()
        {
            RegisterValidationRules();
        }

        protected Substance(): base()
        {
            _dto = new SubstanceDto();
        }

        private Substance(SubstanceDto dto): base()
        {
            ValidateDto(dto);

            if (_dto.SubstanceGroupName == null) return;
            
            var group = SubstanceGroup.Create(new SubstanceGroupDto
                                            {
                                                Id =  _dto.SubstanceGroupId,
                                                Name = _dto.SubstanceGroupName
                                            });
            group.AddSubstance(this);
        }

        public virtual void AddToSubstanceGroup(SubstanceGroup group)
        {
            group.AddSubstance(this);
        }

        public virtual SubstanceGroup SubstanceGroup
        {
            get { return _substanceGroup; }
            internal protected set { _substanceGroup = value; }
        }

        public virtual IEnumerable<Product> Products { get { return _products; } }

        public static Substance Create(SubstanceDto dto)
        {
            return new Substance(dto);
        }

        public static Substance Create(SubstanceDto dto, SubstanceGroup substanceGroup)
        {
            var substance = new Substance(dto);
            if (substanceGroup == null) return substance;
            substance.AddToSubstanceGroup(substanceGroup);
            return substance;
        }

        internal protected virtual void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public virtual void RemoveFromSubstanceGroup()
        {
            SubstanceGroup.Remove(this);
        }

        public override Guid Id { get { return _dto.Id; } protected set { _dto.Id = value; } }

        public override string Name { get { return _dto.Name; } protected set { _dto.Name = value; } }

        private static void RegisterValidationRules()
        {
            ValidationRulesManager.RegisterRule<SubstanceDto>(x => !String.IsNullOrWhiteSpace(x.Name));            
        }

        protected override void SetDto<TDto>(TDto dto)
        {
            var dataTransferObject = dto as DataTransferObject<SubstanceDto>;
            if (dataTransferObject != null) _dto = dataTransferObject.CloneDto();  
        }

        internal protected virtual void ChangeName(string newName)
        {
            Name = newName;
        }
    }
}