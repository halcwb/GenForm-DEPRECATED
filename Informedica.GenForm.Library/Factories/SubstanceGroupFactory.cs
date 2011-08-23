using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.Factories
{
    public class SubstanceGroupFactory : EntityFactory<SubstanceGroup, Guid, SubstanceGroupDto>
    {
        public SubstanceGroupFactory(SubstanceGroupDto dto) : base(dto) {}

        protected override SubstanceGroup Create()
        {
            var group = SubstanceGroup.Create(Dto);
            Add(group);
            return group;
        }
    }
}