using Informedica.GenForm.Library.DomainModel.Data;

namespace Informedica.GenForm.Tests.Fixtures
{
    public static class UnitGroupTestFixtures
    {
        public static UnitGroupDto GetDto()
        {
            return new UnitGroupDto
                       {
                           AllowConversion = true,
                           Name = "massa"
                       };
        }

        public static UnitGroupDto ValidDto()
        {
            return GetDto();
        }
    }
}