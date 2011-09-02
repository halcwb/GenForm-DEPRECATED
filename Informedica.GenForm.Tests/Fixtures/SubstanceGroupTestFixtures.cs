using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Tests.Fixtures
{
    public static class SubstanceGroupTestFixtures
    {
        public static SubstanceGroupDto GetSubstanceGroupDtoWithoutItems()
        {
            return new SubstanceGroupDto {Name = "inotropica"};
        }

        public static bool SubstanceGroupIsValid(SubstanceGroup unitgroup)
        {
            return !String.IsNullOrWhiteSpace(unitgroup.Name);
        }

        public static SubstanceGroup CreateSubstanceGroup()
        {
            return SubstanceGroup.Create(GetSubstanceGroupDtoWithoutItems());
        }
    }
}