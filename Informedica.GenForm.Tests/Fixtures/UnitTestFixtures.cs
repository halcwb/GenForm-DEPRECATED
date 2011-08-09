using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;

namespace Informedica.GenForm.Tests.Fixtures
{
    public static class UnitTestFixtures
    {
        public static UnitDto GetTestUnitMilligram()
        {
            return new UnitDto
                       {
                           Name = "milligram",
                           Abbreviation = "mg",
                           Multiplier = (decimal) 0.0001,
                           Divisor = 0,
                           UnitGroupName = "massa",
                           AllowConversion = true
                       };
        }
    }
}
