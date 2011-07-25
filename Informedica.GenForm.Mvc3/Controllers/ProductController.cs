using System;
using Informedica.GenForm.Library.Services;
using Newtonsoft.Json.Linq;
using System.Web.Mvc;
using Ext.Direct.Mvc;
using Informedica.GenForm.Library.DomainModel.Products;
using StructureMap;

namespace Informedica.GenForm.Mvc3.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult GetGenericNames()
        {
            return this.Direct(
                new[] { new {}} 
            );
        }

        public ActionResult GetBrandNames()
        {
            return this.Direct(new[]
                { 
                    new {}
                }
            );
        }

        public ActionResult GetShapeNames()
        {
            return this.Direct(new[]
                {
                    new {}
                }
            );
        }

        public ActionResult GetPackageNames()
        {
            return this.Direct(new[]
                {
                    new {}
                }
            );
        }

        public ActionResult GetUnitNames()
        {
            return this.Direct(new[]
                {
                    new {}
                }
            );
        }

        public ActionResult GetProducts()
        {
            return this.Direct(new {success = true});
        }

        public ActionResult GetProduct(JObject productId)
        {
            var product = String.IsNullOrEmpty(productId.Value<String>("id")) ? LoadProduct(0) : LoadProduct(Int32.Parse(productId.Value<String>("id")));
            return this.Direct(new { success = true, data = (Object)product ?? new {} });
        }

        public IProduct LoadProduct(Int32 productId)
        {
            var product = new Product
                              {
                                  ProductId = productId,
                                  ProductName = "dopamine (Dynatra) infusievloeistof 200 mg/mL 5 mL ampul",
                                  GenericName = "dopamine",
                                  BrandName = "Dynatra",
                                  ShapeName = "infusievloeistof",
                                  Quantity = 5,
                                  UnitName = "mL",
                                  PackageName = "ampul"
                              };

            return product;
        }

        public ActionResult SaveProduct(JObject productData)
        {
            var success = true;
            var message = String.Empty;
            var product = GetProductFromJObject(productData);
            try
            {
                GetProductServices().SaveProduct(product);

            }
            catch (Exception e)
            {
                success = false;
                message = e.ToString();
            }
            return this.Direct(new { success, data = product, message });
        }

        private IProduct GetProductFromJObject(JObject productData)
        {
            return new Product
                       {
                           ProductName = productData.Value<String>("ProductName"),
                           BrandName = productData.Value<String>("BrandName"),
                           GenericName = productData.Value<String>("GenericName"),
                           PackageName = productData.Value<String>("PackageName"),
                           ProductCode = productData.Value<String>("ProductCode"),
                           ProductId = productData.Value<Int32>("ProductId"),
                           Quantity = productData.Value<Decimal>("Quantity"),
                           ShapeName = productData.Value<String>("ShapeName"),
                           UnitName = productData.Value<String>("UnitName")
                       };
        }

        private IProductServices GetProductServices()
        {
            return ObjectFactory.GetInstance<IProductServices>();
        }

        public ActionResult AddNewBrand(JObject brandDto)
        {
            var success = true;
            var message = String.Empty;
            var brand = GetBrandFromJObject(brandDto);
            
            try
            {
                GetProductServices().AddNewBrand(brand);

            }
            catch (Exception e)
            {
                success = false;
                message = e.ToString();
            }
            
            return this.Direct(new {success, data = brand, message});
        }

        public ActionResult AddNewGeneric(JObject genericDto)
        {
            var success = true;
            var message = String.Empty;
            var generic = GetGenericFromJObject(genericDto);
            
            try
            {
                GetProductServices().AddNewGeneric(generic);

            }
            catch (Exception e)
            {
                success = false;
                message = e.ToString();
            }
            
            return this.Direct(new {success, data = generic, message});
        }

        public ActionResult AddNewShape(JObject shapeDto)
        {
            var success = true;
            var message = String.Empty;
            var shape = GetShapeFromJObject(shapeDto);
            
            try
            {
                GetProductServices().AddNewShape(shape);

            }
            catch (Exception e)
            {
                success = false;
                message = e.ToString();
            }
            
            return this.Direct(new {success, data = shape, message});
        }

        public ActionResult AddNewPackage(JObject packageDto)
        {
            var success = true;
            var message = String.Empty;
            var package = GetPackageFromJObject(packageDto);
            
            try
            {
                GetProductServices().AddNewPackage(package);

            }
            catch (Exception e)
            {
                success = false;
                message = e.ToString();
            }
            
            return this.Direct(new {success, data = package, message});
        }

        public ActionResult AddNewUnit(JObject unitDto)
        {
            var success = true;
            var message = String.Empty;
            var unit = GetUnitFromJObject(unitDto);
            
            try
            {
                GetProductServices().AddNewUnit(unit);

            }
            catch (Exception e)
            {
                success = false;
                message = e.ToString();
            }
            
            return this.Direct(new {success, data = unit, message});
        }

        public ActionResult AddNewSubstance(JObject substanceDto)
        {
            var success = true;
            var message = String.Empty;
            var subst = GetSubstanceFromJObject(substanceDto);
            
            try
            {
                GetProductServices().AddNewSubstance(subst);

            }
            catch (Exception e)
            {
                success = false;
                message = e.ToString();
            }
            
            return this.Direct(new {success, data = subst, message});
        }

        private ISubstance GetSubstanceFromJObject(JObject substanceDto)
        {
            return new Substance {SubstanceName = substanceDto.Value<String>("SubstanceName")};
        }

        private IBrand GetBrandFromJObject(JObject brand)
        {
            return new Brand
                       {
                           BrandName = brand.Value<String>("BrandName")
                       };
        }

        private IShape GetShapeFromJObject(JObject shape)
        {
            return new Shape
                       {
                           ShapeName = shape.Value<String>("ShapeName")
                       };
        }

        private IPackage GetPackageFromJObject(JObject package)
        {
            return new Package
                       {
                           PackageName= package.Value<String>("PackageName")
                       };
        }

        private IGeneric GetGenericFromJObject(JObject generic)
        {
            return new Generic
                       {
                           GenericName = generic.Value<String>("GenericName")
                       };
        }

        private IUnit GetUnitFromJObject(JObject unit)
        {
            return new Unit
                       {
                           UnitName = unit.Value<String>("UnitName")
                       };
        }

    }
}