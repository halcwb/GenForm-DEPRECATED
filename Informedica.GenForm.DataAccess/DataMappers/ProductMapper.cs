using System;
using System.Collections.Generic;
using System.Linq;
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
        private IList<Substance> _newSubstanceList;
        private IList<Unit> _newUnitList;
        private IList<UnitGroup> _newUnitGroupList;

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
            dao.Substance = GetSubstanceDao(bo.GenericName);
            dao.Unit = GetUnitDao(bo.UnitName);
            MapSubstancesFromBoToDao(bo, dao);
        }


        private Unit GetUnitDao(String unitName)
        {
            if (FindNewUnit(unitName) != null) return FindNewUnit(unitName);
            var unit =  new Unit
                {
                    UnitGroup = GetUnitGroupDao("algemeen"),
                    UnitAbbreviation = unitName == String.Empty ? null : unitName,
                    UnitName = unitName,
                    Divisor = 1,
                    IsReference = false,
                    Multiplier = 1
                };

            GetNewUnitList().Add(unit);
            return unit;
        }

        private UnitGroup GetUnitGroupDao(String groupName)
        {
            if (FindNewUnitGroup(groupName) != null) return FindNewUnitGroup(groupName);
            var unitGroup = new UnitGroup
                                {
                                    UnitGroupName = groupName,
                                    AllowsConversion = false
                                };
            GetNewUnitGroupList().Add(unitGroup);
            return unitGroup;
        }

        private UnitGroup FindNewUnitGroup(string groupName)
        {
            return GetNewUnitGroupList().SingleOrDefault(g => g.UnitGroupName == groupName);
        }

        private IList<UnitGroup> GetNewUnitGroupList()
        {
            return _newUnitGroupList ?? (_newUnitGroupList = new List<UnitGroup>());
        }

        private Unit FindNewUnit(string unitName)
        {
            return GetNewUnitList().SingleOrDefault(u => u.UnitName == unitName);
        }

        private IList<Unit> GetNewUnitList()
        {
            return _newUnitList ?? (_newUnitList = new List<Unit>());
        }

        private Substance GetSubstanceDao(String genericName)
        {
            if (FindNewSubstance(genericName) != null) return FindNewSubstance(genericName);
            var substance = new Substance
                                {
                                    SubstanceName = genericName == String.Empty ? null : genericName,
                                    IsGeneric = true
                                };
            GetNewSubstanceList().Add(substance);
            return substance;
        }

        private Substance FindNewSubstance(string genericName)
        {
            return GetNewSubstanceList().SingleOrDefault(s => s.SubstanceName == genericName);
        }

        private IList<Substance> GetNewSubstanceList()
        {
            return _newSubstanceList ?? (_newSubstanceList = new List<Substance>());
        }

        private void MapSubstancesFromBoToDao(IProduct bo, Product dao)
        {
            foreach (var substance in bo.Substances)
            {
                dao.ProductSubstance.Add(CreateNewProductSubstance(substance));
            }
        }

        private ProductSubstance CreateNewProductSubstance(IProductSubstance substance)
        {
            return (new ProductSubstance
                                         {
                                             Substance = GetSubstanceDao(substance.Substance),
                                             SubstanceQuantity = substance.Quantity,
                                             SubstanceOrdering = substance.SortOrder,
                                             Unit = GetUnitDao(substance.Unit)
                                         });
        }

        public void MapFromDaoToBo(Product dao, IProduct bo)
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
