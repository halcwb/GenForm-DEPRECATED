using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Tests.Utilities
{
    public static class ProductChecker
    {
        private static readonly IList<Func<Product, bool>> Checks = new List<Func<Product, bool>>
                                                                {
                                                                    ProductIsValid,
                                                                    ProductHasBrand,
                                                                    ProductHasShape,
                                                                    ProductHasPackage,
                                                                    ProductHasProductSubstance,
                                                                    ProductHasRoutes,
                                                                    ProductHasUnitValue,
                                                                    ProductAssociatesShapeWithPackage,
                                                                    ProductAssociatesShapeWithUnitGroup
                                                                };  

        public static bool CheckAll(Product product)
        {
            return Checks.All(check => check(product));
        }

        public static bool ProductIsValid(Product product)
        {
            return !String.IsNullOrWhiteSpace(product.Name) &&
                   !String.IsNullOrWhiteSpace(product.GenericName) &&
                   !String.IsNullOrWhiteSpace(product.DisplayName) &&
                   !String.IsNullOrWhiteSpace(product.ProductCode);
        }

        public static bool ProductHasBrand(Product product)
        {
            return product.Brand != null &&
                   !String.IsNullOrWhiteSpace(product.Brand.Name) &&
                   product == product.Brand.Products.First();
        }

        public static bool ProductHasShape(Product product)
        {
            return product.Shape != null &&
                   !String.IsNullOrWhiteSpace(product.Shape.Name) &&
                   product == product.Shape.ProductSet.First();
        }

        public static bool ProductHasPackage(Product product)
        {
            return product.Package != null &&
                   !String.IsNullOrWhiteSpace(product.Package.Name) &&
                   product == product.Package.Products.First();
        }

        public static bool ProductAssociatesShapeWithPackage(Product product)
        {
            return product.Shape.PackageSet.Contains(product.Package);
        }

        public static bool ProductHasUnitValue(Product product)
        {
            return product.Quantity != null &&
                   product.Quantity.Value > 0 && 
                   product.Quantity.Unit != null &&
                   !String.IsNullOrWhiteSpace(product.Quantity.Unit.Name);
        }

        public static bool ProductAssociatesShapeWithUnitGroup(Product product)
        {
            return product.Shape.UnitGroupSet.Contains(product.Quantity.Unit.UnitGroup);
        }

        public static bool ProductHasProductSubstance(Product product)
        {
            return product.Substances.Count() > 0 &&
                   product.Substances.First().SortOrder > 0 &&
                   product.Substances.First().Substance != null &&
                   // Weird problem with contains, returns false when in fact same object
                   product == product.Substances.First().Substance.Products.First() &&
                   product.Substances.First().Quantity != null &&
                   product.Substances.First().Quantity.Value > 0 &&
                   !String.IsNullOrWhiteSpace(product.Substances.First().Quantity.Unit.Name);
        }

        public static bool ProductHasRoutes(Product product)
        {
            return product.Routes.Count() > 0 && 
                   !String.IsNullOrWhiteSpace(product.Routes.First().Name) &&
                   !String.IsNullOrWhiteSpace(product.Routes.First().Abbreviation) &&
                   product.Routes.First().Products.Contains(product) &&
                   product.Routes.Last().Products.Contains(product);
        }
    }
}