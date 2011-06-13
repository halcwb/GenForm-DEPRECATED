using System;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using Informedica.GenForm.Library.Services;
using StructureMap.Configuration.DSL;

namespace Informedica.GenForm.Assembler
{
    public static class ProductAssembler
    {
        private static Boolean _hasBeenCalled;
        private static Registry _registry;

        public static Registry RegisterDependencies()
        {
            if (_hasBeenCalled) return _registry;
            _registry = new Registry();

            _registry.For<IProduct>().Use<Product>();
            _registry.For<IProductServices>().Use<ProductServices>();
            _registry.For<IProductRepository>().Use<ProductRepository>();
            _registry.For<IBrand>().Use<Brand>();
            _registry.For<IBrandRepository>().Use<BrandRepository>();
            _registry.For<IGeneric>().Use<Generic>();
            _registry.For<IGenericRepository>().Use<GenericRepository>();
            _registry.For<IShape>().Use<Shape>();
            _registry.For<IShapeRepository>().Use<ShapeRepository>();
            _registry.For<IPackage>().Use<Package>();
            _registry.For<IPackageRepository>().Use<PackageRepository>();
            _registry.For<IUnit>().Use<Unit>();
            _registry.For<IUnitRepository>().Use<UnitRepository>();


            _hasBeenCalled = true;
            return _registry;
        }
    }
}
