using Informedica.GenForm.Library.DomainModel.Products;
using StructureMap.Configuration.DSL;

namespace Informedica.GenForm.Assembler.Assemblers
{
    public static class ProductAssembler
    {
        private static Registry _registry;

        public static Registry RegisterDependencies()
        {
            _registry = new Registry();

            _registry.For<IProduct>().Use<Product>();
            _registry.For<IBrand>().Use<Brand>();
            _registry.For<IGeneric>().Use<Generic>();
            _registry.For<ISubstance>().Use<Substance>();
            _registry.For<IShape>().Use<Shape>();
            _registry.For<IPackage>().Use<Package>();
            _registry.For<IUnit>().Use<Unit>();
            _registry.For<IUnitGroup>().Use<UnitGroup>();

            return _registry;
        }
    }
}
