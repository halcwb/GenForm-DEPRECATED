using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Relations;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Substance : Entity<Guid, SubstanceDto>, ISubstance, IRelationPart
    {
        private readonly HashSet<Product> _products = new HashSet<Product>(new ProductComparer());

        protected Substance(): base(new SubstanceDto()) {}

        private Substance(SubstanceDto substanceDto): base(substanceDto.CloneDto())
        {
            if (Dto.SubstanceGroupName == null) return;
            
            var group = SubstanceGroup.Create(new SubstanceGroupDto
                                            {
                                                Id =  Dto.SubstanceGroupId,
                                                Name = Dto.SubstanceGroupName
                                            });
            group.AddSubstance(this);
        }

        public virtual int SubstanceId
        {
            get { return Dto.SubstanceId; }
            set { Dto.SubstanceId = value; }
        }

        public virtual void AddToSubstanceGroup(SubstanceGroup group)
        {
            RelationProvider.SubstanceGroupSubstance.Add(group, this);
        }

        public virtual SubstanceGroup SubstanceGroup
        {
            get { return RelationProvider.SubstanceGroupSubstance.GetOnePart(this); }
            protected set
            {
                RelationProvider.SubstanceGroupSubstance.Clear(this);
                RelationProvider.SubstanceGroupSubstance.Add(value, this);
            }
        }

        public virtual IEnumerable<Product> Products { get { return _products; } }

        public override bool IdIsDefault(Guid id)
        {
            return id == Guid.Empty;
        }

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
            RelationProvider.SubstanceGroupSubstance.Remove(SubstanceGroup, this);
        }
    }
}