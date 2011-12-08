using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products.Interfaces;
using Informedica.GenForm.Library.Services.Products;
using Informedica.GenForm.Mvc3.Environments;
using Newtonsoft.Json.Linq;
using System.Web.Mvc;
using Ext.Direct.Mvc;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Mvc3.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult GetGenericNames()
        {
            return this.Direct(
                new[] { new { } }
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

        public ActionResult GetRouteNames()
        {
            return this.Direct(new[]
            {
                new {}
            });
        }

        public ActionResult GetProducts()
        {
            return this.Direct(new { success = true });
        }

        [Transaction]
        public ActionResult GetProduct(JObject productId)
        {
            var product = String.IsNullOrEmpty(productId.Value<String>("id")) ? LoadProduct(0) : LoadProduct(Int32.Parse(productId.Value<String>("id")));
            return this.Direct(new { success = true, data = (Object)product ?? new { } });
        }

        public IProduct LoadProduct(Int32 productId)
        {
            var product = ProductServices.WithDto(new ProductDto()).Get();

            return product;
        }

        public ActionResult SaveProduct(ProductDto product)
        {
            var success = true;
            var message = String.Empty;

            try
            {
                var prod = ProductServices.WithDto(product).Get();
            }
            catch (Exception e)
            {
                success = false;
                message = e.ToString();
            }
            return this.Direct(new { success, data = product, message });
        }

        public ActionResult AddNewBrand(JObject brandDto)
        {
            var success = true;
            var message = String.Empty;
            var brand = GetBrandFromJObject(brandDto);

            try
            {
            }
            catch (Exception e)
            {
                success = false;
                message = e.ToString();
            }

            return this.Direct(new { success, data = brand, message });
        }


        public ActionResult AddNewShape(JObject shapeDto)
        {
            var success = true;
            var message = String.Empty;
            var shape = GetShapeFromJObject(shapeDto);

            try
            {
            }
            catch (Exception e)
            {
                success = false;
                message = e.ToString();
            }

            return this.Direct(new { success, data = shape, message });
        }

        public ActionResult AddNewPackage(JObject packageDto)
        {
            var success = true;
            var message = String.Empty;
            var package = GetPackageFromJObject(packageDto);

            try
            {
            }
            catch (Exception e)
            {
                success = false;
                message = e.ToString();
            }

            return this.Direct(new { success, data = package, message });
        }

        [Transaction]
        public ActionResult AddNewUnit(JObject unitDto)
        {
            var success = true;
            var message = String.Empty;
            var unit = GetUnitFromJObject(unitDto);

            try
            {
            }
            catch (Exception e)
            {
                success = false;
                message = e.ToString();
            }

            return this.Direct(new { success, data = unit, message });
        }

        public ActionResult AddNewSubstance(SubstanceDto substanceDto)
        {
            var success = true;
            var message = String.Empty;

            try
            {
            }
            catch (Exception e)
            {
                success = false;
                message = e.ToString();
            }

            return this.Direct(new { success, data = substanceDto, message });
        }

        private Brand GetBrandFromJObject(JObject brand)
        {
            return Brand.Create
                       (new BrandDto
                       {
                           Name = brand.Value<String>("BrandName")
                       });
        }

        private IShape GetShapeFromJObject(JObject shape)
        {
            return Shape.Create(new ShapeDto
                       {
                           Name = shape.Value<String>("ShapeName")
                       });
        }

        private IPackage GetPackageFromJObject(JObject package)
        {
            return Package.Create( new PackageDto
                       {
                           Name = package.Value<String>("PackageName")
                       });
        }

        private IUnit GetUnitFromJObject(JObject unit)
        {
            return UnitServices.WithDto(new UnitDto{ Name = unit.Value<String>("Name")}).Get();
        }

        public ActionResult DeleteProduct(String id)
        {
            Boolean success;
            var message = String.Empty;
            try
            {
                var product = ProductServices.Get(Guid.Parse(id));
                ProductServices.Delete(product);
                success = true;
            }
            catch (Exception e)
            {
                success = false;
                message = e.ToString();
            }

            return this.Direct(new { success, message });
        }
    }
}