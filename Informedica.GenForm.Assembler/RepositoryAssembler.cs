using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.DataAccess.Repositories.Delegates;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Users;
using Informedica.GenForm.Library.Repositories;
using StructureMap.Configuration.DSL;
using Product = Informedica.GenForm.Database.Product;
using Brand = Informedica.GenForm.Database.Brand;
using Package = Informedica.GenForm.Database.Package;
using Shape = Informedica.GenForm.Database.Shape;
using Substance = Informedica.GenForm.Database.Substance;
using Unit = Informedica.GenForm.Database.Unit;

namespace Informedica.GenForm.Assembler
{
    public class RepositoryAssembler
    {
        private static Registry _registry;

        public static Registry RegisterDependencies()
        {
            _registry = new Registry();

            _registry.For<IRepository<IProduct>>().Use<Repository<IProduct>>();
            _registry.For<Insert<Product>>().Use(ProductDelegates.Insert);
            _registry.For<Fetch<Product>>().Use(ProductDelegates.Fetch);
            _registry.For<Delete<Product>>().Use(ProductDelegates.Delete);
            _registry.For<SelectorOfInt<Product>>().Use(ProductDelegates.GetIdSelector);

            _registry.For<IRepository<IBrand>>().Use<Repository<IBrand>>();
            _registry.For<Insert<Brand>>().Use(BrandDelegates.Insert);

            _registry.For<IRepository<IGeneric>>().Use<Repository<IGeneric>>();
            _registry.For<Insert<Substance>>().Use(SubstanceDelegates.Insert);

            _registry.For<IRepository<ISubstance>>().Use<Repository<ISubstance>>();
            _registry.For<Fetch<Substance>>().Use(SubstanceDelegates.Fetch);
            
            _registry.For<IRepository<IShape>>().Use<Repository<IShape>>();
            _registry.For<Insert<Shape>>().Use(ShapeDelegates.Insert);

            _registry.For<IRepository<IPackage>>().Use<Repository<IPackage>>();
            _registry.For<Insert<Package>>().Use(PackageDelegates.Insert);

            _registry.For<IRepository<IUnit>>().Use<Repository<IUnit>>();
            _registry.For<Insert<Unit>>().Use(UnitDelegates.Insert);

            _registry.For<IRepository<IUser>>().Use<Repository<IUser>>();
            _registry.For<Fetch<GenFormUser>>().Use(UserDelegates.Fetch);
            _registry.For<SelectorOfString<GenFormUser>>().Use(UserDelegates.GetNameSelector);

            return _registry;
        }
    }
}