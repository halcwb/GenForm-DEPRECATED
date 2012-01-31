using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Products.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Tests.Utilities
{
    public static class ProductChecker
    {
        private static readonly IList<Func<IProduct, bool>> Checks = new List<Func<IProduct, bool>>
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

        public static bool CheckAll(IProduct product)
        {
            return Checks.All(check => check(product));
        }

        public static bool ProductIsValid(IProduct product)
        {
            var valid = !String.IsNullOrWhiteSpace(product.Name) &&
                        !String.IsNullOrWhiteSpace(product.GenericName) &&
                        !String.IsNullOrWhiteSpace(product.DisplayName) &&
                        !String.IsNullOrWhiteSpace(product.ProductCode);
            if (!valid) throw new AssertFailedException(new StackFrame().GetMethod().Name);
            return true;
        }

        public static bool ProductHasBrand(IProduct product)
        {
            var valid = product.Brand != null &&
                   !String.IsNullOrWhiteSpace(product.Brand.Name) &&
                   product == product.Brand.Products.First();
            if (!valid) throw new AssertFailedException(new StackFrame().GetMethod().Name);
            return true;
        }

        public static bool ProductHasShape(IProduct product)
        {
            var valid = product.Shape != null &&
                   !String.IsNullOrWhiteSpace(product.Shape.Name) &&
                   product == product.Shape.Products.First();
            if (!valid) throw new AssertFailedException(new StackFrame().GetMethod().Name);
            return true;
        }

        public static bool ProductHasPackage(IProduct product)
        {
            var valid = product.Package != null &&
                        !String.IsNullOrWhiteSpace(product.Package.Name) &&
                        product == product.Package.Products.First();
            if (!valid) throw new AssertFailedException(new StackFrame().GetMethod().Name);
            return true;
        }

        public static bool ProductAssociatesShapeWithPackage(IProduct product)
        {
            var valid = product.Shape.Packages.Contains(product.Package);
            if (!valid) throw new AssertFailedException(new StackFrame().GetMethod().Name);
            return true;
        }

        public static bool ProductHasUnitValue(IProduct product)
        {
            var valid = product.Quantity != null &&
                   product.Quantity.Value > 0 &&
                   product.Quantity.Unit != null &&
                   !String.IsNullOrWhiteSpace(product.Quantity.Unit.Name);
            if (!valid) throw new AssertFailedException(new StackFrame().GetMethod().Name);
            return true;
        }

        public static bool ProductAssociatesShapeWithUnitGroup(IProduct product)
        {
            if (product.Quantity.Unit.UnitGroup == null) return false;
            var valid = product.Shape.UnitGroups.Contains(product.Quantity.Unit.UnitGroup);
            if (!valid) throw new AssertFailedException(new StackFrame().GetMethod().Name);
            return true;
        }

        public static bool ProductHasProductSubstance(IProduct product)
        {
            var valid = product.Substances.Any() &&
                   product.Substances.First().SortOrder > 0 &&
                   product.Substances.First().Substance != null &&
                   product.Substances.First().Substance.Products.Contains(product) &&
                   product.Substances.First().Quantity != null &&
                   product.Substances.First().Quantity.Value > 0 &&
                   !String.IsNullOrWhiteSpace(product.Substances.First().Quantity.Unit.Name);
            if (!valid) throw new AssertFailedException(new StackFrame().GetMethod().Name);
            return true;
        }

        public static bool ProductHasRoutes(IProduct product)
        {
            var valid = product.Routes.Any() &&
                   !String.IsNullOrWhiteSpace(product.Routes.First().Name) &&
                   !String.IsNullOrWhiteSpace(product.Routes.First().Abbreviation) &&
                   product.Routes.First().Products.Contains(product) &&
                   product.Routes.Last().Products.Contains(product);
            if (!valid) throw new AssertFailedException(new StackFrame().GetMethod().Name);
            return true;
        }
    }
}