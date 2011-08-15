using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;

namespace Informedica.GenForm.Library.Factories
{
    public static class UnitGroupFactory
    {
        public static UnitGroup CreateUnitGroup(UnitGroupDto dto)
        {
// ReSharper disable CSharpWarnings::CS0612
            return new UnitGroup(dto);
// ReSharper restore CSharpWarnings::CS0612
        }
    }
}