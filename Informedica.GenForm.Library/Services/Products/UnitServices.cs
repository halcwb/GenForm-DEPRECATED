using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.Factories;

namespace Informedica.GenForm.Library.Services.Products
{
    public static class UnitServices
    {
        public static UnitCreator WithDto(UnitDto dto)
        {
            return new UnitCreator(dto);
        }

        public static Unit GetUnit(Guid id)
        {
            return Repository.SingleOrDefault(x => x.Id == id);
        }

        private static IEnumerable<Unit> Repository
        {
            get { return RepositoryFactory.Create<Unit, Guid, UnitDto>(); }
        }


    }
}