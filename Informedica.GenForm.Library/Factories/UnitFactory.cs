using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;

namespace Informedica.GenForm.Library.Factories
{
    public class UnitFactory
    {
        public static Unit CreateUnit(UnitDto dto, UnitGroup group)
        {
            return new Unit(dto, group);
        }

        public static Unit CreateUnit(UnitDto dto)
        {
            return new Unit(dto, UnitGroupFactory.CreateUnitGroup(
                new UnitGroupDto
                {
                    AllowConversion = dto.AllowConversion,
                    Name = dto.UnitGroupName
                }));
        }

        public static Unit CreateUnit(UnitDto dto, UnitGroupDto groupDto)
        {
            return new Unit(dto, UnitGroupFactory.CreateUnitGroup(
                new UnitGroupDto
                {
                    AllowConversion = groupDto.AllowConversion,
                    Name = groupDto.Name
                }));
        }
    }
}
