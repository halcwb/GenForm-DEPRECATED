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

            _hasBeenCalled = true;
            return _registry;
        }
    }
}
