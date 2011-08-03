using Informedica.GenForm.DataAccess.DataMappers;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using Informedica.GenForm.Library.Services.Products;
using StructureMap.Configuration.DSL;

namespace Informedica.GenForm.Assembler
{
    public static class ProductAssembler
    {
        private static Registry _registry;

        public static Registry RegisterDependencies()
        {
            _registry = new Registry();

            _registry.For<IProduct>().Use<Product>();
            _registry.For<IProductServices>().Use<ProductServices>();
            _registry.For<IProductRepository>().Use<ProductRepository>();
            _registry.SelectConstructor(() => new ProductRepository());
            _registry.For<IDataMapper<IProduct, Database.Product>>().Use<ProductMapper>();
            _registry.For<IBrand>().Use<Brand>();
            _registry.For<IBrandRepository>().Use<BrandRepository>();
            _registry.SelectConstructor(() => new BrandRepository());
            _registry.For<IDataMapper<IBrand,Database.Brand>>().Use<BrandMapper>();
            _registry.For<IGeneric>().Use<Generic>();
            _registry.For<IGenericRepository>().Use<GenericRepository>();
            _registry.SelectConstructor(() => new GenericRepository());
            _registry.For<IDataMapper<IGeneric, Database.Substance>>().Use<GenericMapper>();
            _registry.For<IShape>().Use<Shape>();
            _registry.For<IShapeRepository>().Use<ShapeRepository>();
            _registry.SelectConstructor(() => new ShapeRepository());
            _registry.For<IDataMapper<IShape, Database.Shape>>().Use<ShapeMapper>();
            _registry.For<IPackage>().Use<Package>();
            _registry.For<IPackageRepository>().Use<PackageRepository>();
            _registry.SelectConstructor(() => new PackageRepository());
            _registry.For<IUnit>().Use<Unit>();
            _registry.For<IUnitRepository>().Use<UnitRepository>();
            _registry.SelectConstructor(() => new UnitRepository());
            _registry.For<IDataMapper<IUnit, Database.Unit>>().Use<UnitMapper>();
            _registry.For<ISubstance>().Use<Substance>();
            _registry.For<ISubstanceRepository>().Use<SubstanceRepository>();
            _registry.SelectConstructor(() => new SubstanceRepository());
            _registry.For<IDataMapper<ISubstance, Database.Substance>>().Use<SubstanceMapper>();

            return _registry;
        }
    }
}
