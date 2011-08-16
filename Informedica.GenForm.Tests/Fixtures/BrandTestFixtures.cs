using Informedica.GenForm.Library.DomainModel.Data;

namespace Informedica.GenForm.Tests.Fixtures
{
    public static class BrandTestFixtures
    {
        public static BrandDto GetDto()
        {
            return new BrandDto
                       {
                           Name = "Dynatra"
                       };
        }
    }
}