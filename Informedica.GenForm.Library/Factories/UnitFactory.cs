using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;

namespace Informedica.GenForm.Library.Factories
{
    public class UnitFactory : EntityFactory<Unit, Guid, UnitDto>
    {
        private UnitGroup _group;

        public UnitFactory(UnitDto dto) : base(dto) {}

        protected override Unit Create()
        {
            if (_group == null) _group = GetGroup(new UnitGroupDto
                {
                    Name =  Dto.UnitGroupName,
                    AllowConversion = Dto.AllowConversion
                });

            var unit = Unit.Create(Dto, _group);
            Add(unit);
            return unit;
        }

        public UnitFactory AddToGroup(UnitGroupDto groupDto)
        {
            _group = GetGroup(groupDto);
            return this;
        }

        private UnitGroup GetGroup(UnitGroupDto groupDto)
        {
            return new UnitGroupFactory(groupDto).Get();
        }
    }
}