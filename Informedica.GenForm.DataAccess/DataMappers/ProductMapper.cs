using System;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Brand = Informedica.GenForm.Database.Brand;
using Package = Informedica.GenForm.Database.Package;
using Product = Informedica.GenForm.Database.Product;
using ProductSubstance = Informedica.GenForm.Database.ProductSubstance;
using Shape = Informedica.GenForm.Database.Shape;
using Substance = Informedica.GenForm.Database.Substance;
using Unit = Informedica.GenForm.Database.Unit;

namespace Informedica.GenForm.DataAccess.DataMappers
{
    public class ProductMapper : IDataMapper<IProduct, Product>
    {
        #region Implementation of IDataMapper<IProduct,Product>

        public void MapFromBoToDao(IProduct bo, Product dao)
        {
            dao.Brand = new Brand { BrandName = bo.BrandName == String.Empty ? null : bo.BrandName };
            dao.DisplayName = bo.DisplayName == String.Empty ? bo.ProductName : bo.DisplayName;
            dao.Divisor = 1;
            dao.Package = new Package { PackageName = bo.PackageName == String.Empty ? null : bo.PackageName };
            dao.ProductCode = "1";
            dao.ProductKey = "1";
            dao.ProductName = bo.DisplayName;
            dao.ProductQuantity = 5;
            dao.Shape = new Shape { ShapeName = bo.ShapeName == String.Empty ? null : bo.ShapeName };
            dao.Substance = CreateNewSubstanceDao(bo.GenericName);
            dao.Unit = CreateNewUnitDao(bo.UnitName);
            MapSubstancesFromBoToDao(bo, dao);
        }

        public void MapFromDaoToBo(Product dao, IProduct bo)
        {
            throw new NotImplementedException();
        }

        private static Unit CreateNewUnitDao(String unitName)
        {
            return new Unit
            {
                UnitGroup = new UnitGroup
                {
                    UnitGroupName = "Verpakking",
                    AllowsConversion = false
                },
                UnitAbbreviation = unitName == String.Empty ? null : unitName,
                UnitName = unitName,
                Divisor = 1,
                IsReference = false,
                Multiplier = 1
            };
        }

        private static Substance CreateNewSubstanceDao(String genericName)
        {
            return new Substance
                                {
                                    SubstanceName = genericName == String.Empty ? null : genericName,
                                    IsGeneric = true
                                };
        }

        private static void MapSubstancesFromBoToDao(IProduct bo, Product dao)
        {
            foreach (var substance in bo.Substances)
            {
                dao.ProductSubstance.Add(CreateNewProductSubstance(substance));
            }
        }

        private static ProductSubstance CreateNewProductSubstance(IProductSubstance substance)
        {
            return (new ProductSubstance
                                         {
                                             Substance = CreateNewSubstanceDao(substance.Substance),
                                             SubstanceQuantity = substance.Quantity,
                                             SubstanceOrdering = substance.SortOrder,
                                             Unit = CreateNewUnitDao(substance.Unit)
                                         });
        }

        public void MapFromDaoToBo(Product dao, IMappable<IProduct> bo)
        {
            bo.BrandName = dao.Brand == null ? String.Empty : dao.Brand.BrandName;
            bo.GenericName = dao.Substance.SubstanceName;
            bo.PackageName = dao.Package.PackageName;
            bo.ProductCode = dao.ProductCode;
            bo.ProductId = dao.ProductId;
            bo.ProductName = dao.ProductName;
            bo.DisplayName = dao.DisplayName;
            bo.Quantity = dao.ProductQuantity ?? Decimal.Zero;
            bo.ShapeName = dao.Shape.ShapeName;
            bo.UnitName = dao.Unit.UnitName;
        }

        #endregion
    }
}
