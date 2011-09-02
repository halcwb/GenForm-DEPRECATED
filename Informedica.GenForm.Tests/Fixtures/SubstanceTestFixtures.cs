using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Tests.Fixtures
{
    public static class SubstanceTestFixtures
    {
        public static IEnumerable<SubstanceDto> GetSubstanceDtoListWithThreeItems()
        {
            var list = new List<SubstanceDto>();
            list.Add(new SubstanceDto{Name = "paracetamol"});
            list.Add(new SubstanceDto{Name = "dopamine"});
            list.Add(new SubstanceDto{Name = "lactulose"});

            return list;
        }

        public static SubstanceDto GetSubstanceWithGroup()
        {
            return new SubstanceDto
                       {
                           Name = "dopamine",
                           SubstanceGroupName = "intropica"
                       };
        }

        public static SubstanceDto GetSubstanceDtoWithoutGroup()
        {
            var dto = GetSubstanceWithGroup();
            dto.SubstanceGroupName = "";
            return dto;
        }

        public static Substance CreateSubstanceWithoutGroup()
        {
            return Substance.Create(GetSubstanceDtoWithoutGroup());
        }

        public static Substance CreateSubstanceWitGroup()
        {
            var group = SubstanceGroupTestFixtures.CreateSubstanceGroup();
            return Substance.Create(GetSubstanceDtoWithoutGroup(), group);
        }
    }
}
