using System;
using Informedica.GenForm.Library.Services;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
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
            return this.Direct( new [] { new  {GenericName = "paracetamol"}, new {GenericName = "dopamine"} });
        }

        public ActionResult GetBrandNames()
        {
            return this.Direct( new [] { new  {BrandName = "Paracetamol"}, new {BrandName = "Dynatra"} });
        }

        public ActionResult GetProducts()
        {
            return this.Direct(new List<IProduct> {LoadProduct(0)});
        }

        public ActionResult GetProduct(JObject productId)
        {
            var product = productId.Value<String>("id") == null ? LoadProduct(0): LoadProduct(Int32.Parse(productId.Value<String>("id")));
            return this.Direct(new {success = true, data = product});
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
            IProduct product = new Product
                              {
                                  ProductName = productData.Value<String>("ProductName"),
                                  BrandName = productData.Value<String>("BrandName"),
                                  GenericName = productData.Value<String>("GenericName"),
                                  PackageName = productData.Value<String>("PackageName"),
                                  ProductCode = productData.Value<String>("ProductCode"),
                                  ProductId = productData.Value<Int32>("ProductId"),
                                  Quantity = productData.Value<Double>("Quantity"),
                                  ShapeName = productData.Value<String>("ShapeName"),
                                  UnitName = productData.Value<String>("UnitName")
                              };
            try
            {
                GetProductServices().SaveProduct(product);

            }
            catch (Exception e)
            {
                success = false;
                message = e.ToString();
            }
            return this.Direct(new {success, data = product, message});
        }

        private IProductServices GetProductServices()
        {
            return ObjectFactory.GetInstance<IProductServices>();
        }
    }
}