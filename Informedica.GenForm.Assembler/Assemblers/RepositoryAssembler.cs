using System;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.Repositories;
using StructureMap.Configuration.DSL;

namespace Informedica.GenForm.Assembler.Assemblers
{
    public class RepositoryAssembler
    {
        private static Registry _registry;

        public static Registry RegisterDependencies()
        {
            _registry = new Registry();

            _registry.For<IRepository<Brand, Guid, BrandDto>>().Use<BrandRepository>();
            _registry.For<IRepository<Shape, Guid, ShapeDto>>().Use<ShapeRepository>();
            _registry.For<IRepository<Package, Guid, PackageDto>>().Use<PackageRepository>();
            _registry.For<IRepository<Route, Guid, RouteDto>>().Use<RouteRepository>();
            _registry.For<IRepository<Unit, Guid, UnitDto>>().Use<UnitRepository>();
            _registry.For<IRepository<UnitGroup, Guid, UnitGroupDto>>().Use<UnitGroupRepository>();
            _registry.For<IRepository<Substance, Guid, SubstanceDto>>().Use<SubstanceRepository>();
            _registry.For<IRepository<SubstanceGroup, Guid, SubstanceGroupDto>>().Use<SubstanceGroupRepository>();
            _registry.For<IRepository<Product, Guid, ProductDto>>().Use<ProductRepository>();

            return _registry;
        }
    }
}