using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Library.DomainModel.Products;
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

            _registry.For<IRepository<Brand>>().Use<BrandRepository>();
            _registry.For<IRepository<Shape>>().Use<ShapeRepository>();
            _registry.For<IRepository<Package>>().Use<PackageRepository>();
            _registry.For<IRepository<Route>>().Use<RouteRepository>();
            _registry.For<IRepository<Unit>>().Use<UnitRepository>();
            _registry.For<IRepository<UnitGroup>>().Use<UnitGroupRepository>();
            _registry.For<IRepository<Substance>>().Use<SubstanceRepository>();
            _registry.For<IRepository<SubstanceGroup>>().Use<SubstanceGroupRepository>();
            _registry.For<IRepository<Product>>().Use<ProductRepository>();

            return _registry;
        }
    }
}