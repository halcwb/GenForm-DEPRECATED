using Informedica.GenForm.Library.DomainModel.Data;

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
                           Multiplier = 0.0001M,
                           Divisor = 1000,
                           UnitGroupName = "massa",
                           AllowConversion = true
                       };
        }

        public static UnitDto GetTestUnitMililiter()
        {
            return  new UnitDto
                        {
                            Abbreviation = "mL",
                            AllowConversion = true,
                            Divisor = 1000,
                            IsReference = false,
                            Multiplier = 0.001M,
                            Name = "milliliter",
                            UnitGroupName = "volume"
                        };
        }
    }
}
