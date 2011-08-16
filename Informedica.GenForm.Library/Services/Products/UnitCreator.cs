using System;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.Factories;
using Informedica.GenForm.Library.Repositories;

namespace Informedica.GenForm.Library.Services.Products
{
    public class UnitCreator
    {
        private readonly UnitDto _dto;
        private UnitGroupDto _groupDto;
        private IRepository<Unit, Guid, UnitDto> _repository;

        public UnitCreator(UnitDto dto)
        {
            _dto = dto;
        }

        public Unit GetUnit()
        {
            return FindUnit() ?? CreateUnit();
        }

        private Unit FindUnit()
        {
            return Repository.SingleOrDefault(x => x.Name == _dto.Name);
        }

        private IRepository<Unit, Guid, UnitDto> Repository
        {
            get { return _repository ?? (_repository = RepositoryFactory.Create<Unit, Guid, UnitDto>()); }
        }

        private Unit CreateUnit()
        {
            AddNewUnitToRepository();
            return FindUnit();
        }

        private void AddNewUnitToRepository()
        {
            Repository.Add(_groupDto != null
                               ? Unit.Create(_dto, _groupDto)
                               : Unit.Create(_dto));
        }

        public UnitCreator AddToGroup(UnitGroupDto groupDto)
        {
            _groupDto = groupDto;
            return this;
        }
    }
}