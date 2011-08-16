using System;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.DataAccess.Repositories.Delegates;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.DomainModel.Users;
using Informedica.GenForm.Library.Repositories;
using StructureMap.Configuration.DSL;
using Brand = Informedica.GenForm.Database.Brand;
using Package = Informedica.GenForm.Database.Package;
using Product = Informedica.GenForm.Database.Product;
using Shape = Informedica.GenForm.Database.Shape;
using Substance = Informedica.GenForm.Database.Substance;
using Unit = Informedica.GenForm.Library.DomainModel.Products.Unit;
using UnitGroup = Informedica.GenForm.Library.DomainModel.Products.UnitGroup;

namespace Informedica.GenForm.Assembler.Assemblers
{
    public class RepositoryAssembler
    {
        private static Registry _registry;

        public static Registry RegisterDependencies()
        {
            _registry = new Registry();

            _registry.For<IRepositoryLinqToSql<IProduct>>().Use<RepositoryLinqToSql<IProduct>>();
            _registry.For<Insert<Product>>().Use(ProductDelegates.Insert);
            _registry.For<Fetch<Product>>().Use(ProductDelegates.Fetch);
            _registry.For<Delete<Product>>().Use(ProductDelegates.Delete);
            _registry.For<SelectorOfInt<Product>>().Use(ProductDelegates.GetIdSelector);

            _registry.For<IRepositoryLinqToSql<IBrand>>().Use<RepositoryLinqToSql<IBrand>>();
            _registry.For<Insert<Brand>>().Use(BrandDelegates.Insert);

            _registry.For<IRepositoryLinqToSql<IGeneric>>().Use<RepositoryLinqToSql<IGeneric>>();
            _registry.For<Insert<Substance>>().Use(SubstanceDelegates.Insert);

            _registry.For<IRepositoryLinqToSql<ISubstance>>().Use<RepositoryLinqToSql<ISubstance>>();
            _registry.For<Fetch<Substance>>().Use(SubstanceDelegates.Fetch);
            
            _registry.For<IRepositoryLinqToSql<IShape>>().Use<RepositoryLinqToSql<IShape>>();
            _registry.For<Insert<Shape>>().Use(ShapeDelegates.Insert);

            _registry.For<IRepositoryLinqToSql<IPackage>>().Use<RepositoryLinqToSql<IPackage>>();
            _registry.For<Insert<Package>>().Use(PackageDelegates.Insert);

            _registry.For<IRepositoryLinqToSql<IUnit>>().Use<RepositoryLinqToSql<IUnit>>();
            _registry.For<IRepository<Unit, Guid, UnitDto>>().Use<UnitRepository>();

            _registry.For<IRepository<UnitGroup, Guid, UnitGroupDto>>().Use<UnitGroupRepository>();

            _registry.For<IRepositoryLinqToSql<IUser>>().Use<RepositoryLinqToSql<IUser>>();
            _registry.For<Fetch<GenFormUser>>().Use(UserDelegates.Fetch);
            _registry.For<SelectorOfString<GenFormUser>>().Use(UserDelegates.GetNameSelector);

            return _registry;
        }
    }
}