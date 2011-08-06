﻿using Informedica.GenForm.DataAccess.DataMappers;
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

            _registry.For<IRepository<IProduct>>().Use<Repository<IProduct, Product>>();
            _registry.For<IDataMapper<IProduct, Product>>().Use<ProductMapper>();
            _registry.For<Insert<Product>>().Use(ProductDelegates.Insert);
            _registry.For<Refresh<IProduct,Product>>().Use(ProductDelegates.Refresh);
            _registry.For<Fetch<Product>>().Use(ProductDelegates.Fetch);
            _registry.For<Delete<Product>>().Use(ProductDelegates.Delete);
            _registry.For<SelectorOfInt<Product>>().Use(ProductDelegates.GetIdSelector);

            _registry.For<IRepository<IBrand>>().Use<Repository<IBrand, Brand>>();
            _registry.For<IDataMapper<IBrand, Brand>>().Use<BrandMapper>();
            _registry.For<Insert<Brand>>().Use(BrandDelegates.InsertOnSubmit);
            _registry.For<Refresh<IBrand, Brand>>().Use(BrandDelegates.UpdateBo);

            _registry.For<IRepository<IGeneric>>().Use<Repository<IGeneric, Substance>>();
            _registry.For<IDataMapper<IGeneric, Substance>>().Use<GenericMapper>();
            _registry.For<Insert<Substance>>().Use(SubstanceDelegates.InsertOnSubmit);
            _registry.For<Refresh<IGeneric, Substance>>().Use(GenericDelegates.UpdateBo);

            _registry.For<IRepository<ISubstance>>().Use<Repository<ISubstance, Substance>>();
            _registry.For<IDataMapper<ISubstance, Substance>>().Use<SubstanceMapper>();
            _registry.For<Fetch<Substance>>().Use(SubstanceDelegates.Fetch);
            _registry.For<Refresh<ISubstance, Substance>>().Use(SubstanceDelegates.UpdateBo);
            
            _registry.For<IRepository<IShape>>().Use<Repository<IShape, Shape>>();
            _registry.For<IDataMapper<IShape, Shape>>().Use<ShapeMapper>();
            _registry.For<Insert<Shape>>().Use(ShapeDelegates.InsertOnSubmit);
            _registry.For<Refresh<IShape, Shape>>().Use(ShapeDelegates.UpdateBo);

            _registry.For<IRepository<IPackage>>().Use<Repository<IPackage, Package>>();
            _registry.For<IDataMapper<IPackage, Package>>().Use<PackageMapper>();
            _registry.For<Insert<Package>>().Use(PackageDelegates.InsertOnSubmit);
            _registry.For<Refresh<IPackage, Package>>().Use(PackageDelegates.UpdateBo);

            _registry.For<IRepository<IUnit>>().Use<Repository<IUnit, Unit>>();
            _registry.For<IDataMapper<IUnit, Unit>>().Use<UnitMapper>();
            _registry.For<Insert<Unit>>().Use(UnitDelegates.InsertOnSubmit);
            _registry.For<Refresh<IUnit, Unit>>().Use(UnitDelegates.UpdateBo);

            _registry.For<IRepository<IUser>>().Use<Repository<IUser, GenFormUser>>();
            _registry.For<IDataMapper<IUser, GenFormUser>>().Use<UserMapper>();
            _registry.For<Fetch<GenFormUser>>().Use(UserDelegates.Fetch);
            _registry.For<SelectorOfString<GenFormUser>>().Use(UserDelegates.CreateNameSelector);

            return _registry;
        }
    }
}