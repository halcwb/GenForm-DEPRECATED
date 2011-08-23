using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.Factories
{
    public class SubstanceFactory : EntityFactory<Substance, Guid, SubstanceDto>
    {
        public SubstanceFactory(SubstanceDto dto) : base(dto) {}

        protected override Substance Create()
        {
            var substance = Substance.Create(Dto, GetSubstanceGroup());
            Add(substance);
            return substance;
        }

        private SubstanceGroup GetSubstanceGroup()
        {
            if (String.IsNullOrWhiteSpace(Dto.SubstanceGroupName)) return null;
            return new SubstanceGroupFactory(GetSubstanceGroupDto()).Get();
        }

        private SubstanceGroupDto GetSubstanceGroupDto()
        {
            return new SubstanceGroupDto
                       {
                           Name = Dto.SubstanceGroupName
                       };
        }
    }
}