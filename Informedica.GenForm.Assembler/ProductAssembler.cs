using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Informedica.GenForm.IoC;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using Informedica.GenForm.Library.Services;

namespace Informedica.GenForm.Assembler
{
    public static class ProductAssembler
    {
        private static Boolean _hasBeenCalled;

        public static void RegisterDependencies()
        {
            if (_hasBeenCalled) return;

            LibraryRegistry.RegisterTypeFor<IProduct, Product>();
            LibraryRegistry.RegisterTypeFor<IProductServices, ProductServices>();
            LibraryRegistry.RegisterTypeFor<IProductRepository, ProductRepository>();

            _hasBeenCalled = true;
        }
    }
}
