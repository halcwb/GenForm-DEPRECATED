using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Product = Informedica.GenForm.Database.Product;

namespace Informedica.GenForm.DataAccess.DataMappers
{
    public class ProductMapper: IDataMapper<IProduct, Product>
    {
        #region Implementation of IDataMapper<IProduct,Product>

        public void MapFromBoToDao(IProduct bo, Product dao)
        {
            dao.Brand = new Brand {BrandName = bo.BrandName == String.Empty ? null: bo.BrandName};
            dao.DisplayName = bo.ProductName == String.Empty ? null: bo.ProductName;
            dao.Divisor = 1;
            dao.Package = new Package {PackageName = bo.PackageName == String.Empty ? null : bo.ProductName};
            dao.ProductCode = "1";
            dao.ProductKey = "1";
            dao.ProductName = dao.DisplayName;
            dao.ProductQuantity = 5;
            dao.Shape = new Shape {ShapeName = bo.ShapeName == String.Empty ? null : bo.ShapeName};
            dao.Substance = new Substance
                                {
                                    SubstanceName = bo.GenericName == String.Empty ? null : bo.GenericName, 
                                    IsGeneric = true
                                };
            dao.Unit = new Unit
                           {
                               UnitGroup = new UnitGroup
                                               {
                                                   UnitGroupName = "Verpakking", 
                                                   AllowsConversion = false
                                               },
                               UnitAbbreviation = bo.UnitName == String.Empty ? null : bo.UnitName,
                               UnitName = bo.UnitName,
                               Divisor = 1,
                               IsReference = false,
                               Multiplier = 1
                           };
        }

        public void MapFromDaoToBo(Product dao, IProduct bo)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
