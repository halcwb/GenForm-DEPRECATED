using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var valid = !String.IsNullOrWhiteSpace(product.Name) &&
                        !String.IsNullOrWhiteSpace(product.GenericName) &&
                        !String.IsNullOrWhiteSpace(product.DisplayName) &&
                        !String.IsNullOrWhiteSpace(product.ProductCode);
            if (!valid) throw new AssertFailedException(new StackFrame().GetMethod().Name);
            return true;
        }

        public static bool ProductHasBrand(Product product)
        {
            var valid = product.Brand != null &&
                   !String.IsNullOrWhiteSpace(product.Brand.Name) &&
                   product == product.Brand.Products.First();
            if (!valid) throw new AssertFailedException(new StackFrame().GetMethod().Name);
            return true;
        }

        public static bool ProductHasShape(Product product)
        {
            var valid = product.Shape != null &&
                   !String.IsNullOrWhiteSpace(product.Shape.Name) &&
                   product == product.Shape.ProductSet.First();
            if (!valid) throw new AssertFailedException(new StackFrame().GetMethod().Name);
            return true;
        }

        public static bool ProductHasPackage(Product product)
        {
            var valid = product.Package != null &&
                        !String.IsNullOrWhiteSpace(product.Package.Name) &&
                        product == product.Package.ProductSet.First();
            if (!valid) throw new AssertFailedException(new StackFrame().GetMethod().Name);
            return true;
        }

        public static bool ProductAssociatesShapeWithPackage(Product product)
        {
            var valid = product.Shape.PackageSet.Contains(product.Package);
            if (!valid) throw new AssertFailedException(new StackFrame().GetMethod().Name);
            return true;
        }

        public static bool ProductHasUnitValue(Product product)
        {
            var valid = product.Quantity != null &&
                   product.Quantity.Value > 0 &&
                   product.Quantity.Unit != null &&
                   !String.IsNullOrWhiteSpace(product.Quantity.Unit.Name);
            if (!valid) throw new AssertFailedException(new StackFrame().GetMethod().Name);
            return true;
        }

        public static bool ProductAssociatesShapeWithUnitGroup(Product product)
        {
            if (product.Quantity.Unit.UnitGroup == null) return false;
            var valid = product.Shape.UnitGroups.Contains(product.Quantity.Unit.UnitGroup);
            if (!valid) throw new AssertFailedException(new StackFrame().GetMethod().Name);
            return true;
        }

        public static bool ProductHasProductSubstance(Product product)
        {
            var valid = product.Substances.Count() > 0 &&
                   product.Substances.First().SortOrder > 0 &&
                   product.Substances.First().Substance != null &&
                   product.Substances.First().Substance.Products.Contains(product) &&
                   product.SubstanceList.First().Quantity != null &&
                   product.SubstanceList.First().Quantity.Value > 0 &&
                   !String.IsNullOrWhiteSpace(product.SubstanceList.First().Quantity.Unit.Name);
            if (!valid) throw new AssertFailedException(new StackFrame().GetMethod().Name);
            return true;
        }

        public static bool ProductHasRoutes(Product product)
        {
            var valid = product.RouteSet.Count() > 0 &&
                   !String.IsNullOrWhiteSpace(product.Routes.First().Name) &&
                   !String.IsNullOrWhiteSpace(product.Routes.First().Abbreviation) &&
                   product.Routes.First().Products.Contains(product) &&
                   product.Routes.Last().Products.Contains(product);
            if (!valid) throw new AssertFailedException(new StackFrame().GetMethod().Name);
            return true;
        }
    }
}