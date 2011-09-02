using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Tests.Fixtures
{
    public static class UnitGroupTestFixtures
    {
        public static UnitGroupDto GetDtoMass()
        {
            return new UnitGroupDto
                       {
                           AllowConversion = true,
                           Name = "massa"
                       };
        }

        public static UnitGroupDto GetDtoVolume()
        {
            return new UnitGroupDto
            {
                AllowConversion = true,
                Name = "volume"
            };
        }

        public static UnitGroup CreateUnitGroupVolume()
        {
            return UnitGroup.Create(GetDtoVolume());
        }

        public static UnitGroup CreateUnitGroupMass()
        {
            return UnitGroup.Create(GetDtoMass());
        }
    }
}