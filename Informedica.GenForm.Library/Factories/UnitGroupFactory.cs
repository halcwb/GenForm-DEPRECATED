using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.Factories
{
    public class UnitGroupFactory : EntityFactory<UnitGroup, Guid, UnitGroupDto>
    {
        public UnitGroupFactory(UnitGroupDto dto) : base(dto) {}

        protected override UnitGroup Create()
        {
            var group = UnitGroup.Create(Dto);
            Add(group);
            return Find();
        }
    }
}
