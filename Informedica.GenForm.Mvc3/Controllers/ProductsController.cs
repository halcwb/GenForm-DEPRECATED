using System;
using System.Linq;
using System.Web.Mvc;
using Ext.Direct.Mvc;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Dispense;
using Informedica.GenForm.Library.DomainModel.Import;
using Informedica.GenForm.Library.Services;
using Informedica.GenForm.Presentation.Products;

namespace Informedica.GenForm.Mvc2.Controllers
{
    public class ProductsController : Controller
    {

        public ActionResult GetProductList(Newtonsoft.Json.Linq.JObject paging)
        {
            int start = Int32.Parse(paging.Value<string>("start"));
            int limit = Int32.Parse(paging.Value<string>("limit"));
            string filter = paging.Value<string>("filter");

            ProductInfoList list = ProductInfoList.GetProductInfoList(start, limit);
            // temporary limit, have to inmplement a paging mechanism.
            return this.Direct(new { totalCount = list.TotalCount, records = list });
        }

        public ActionResult GetProduct(int productId)
        {
            Product product = Product.GetProduct(productId);
            return this.Direct(new { success = true, data = product });
        }

        public ActionResult GetSubstanceList()
        {
            SubstanceNameList list = SubstanceNameList.GetNameValueList();
            return this.Direct(list);
        }

        public ActionResult GetGenericList()
        {
            Informedica.GenForm.Library.DomainModel.Lists.GenericNameList list = Informedica.GenForm.Library.DomainModel.Lists.GenericNameList.GetNameValueList();
            return this.Direct(list);
        }

        public ActionResult GetShapeList()
        {
            Informedica.GenForm.Library.DomainModel.Dispense.ShapeNameList list = Informedica.GenForm.Library.DomainModel.Dispense.ShapeNameList.GetNameValueList();
            return this.Direct(list);
        }

        public ActionResult GetPackageList()
        {
            Informedica.GenForm.Library.DomainModel.Lists.PackageNameList list = Informedica.GenForm.Library.DomainModel.Lists.PackageNameList.GetNameValueList();
            return this.Direct(list);
        }

        public ActionResult GetShapeUnitList()
        {
            Informedica.GenForm.Library.DomainModel.Lists.UnitNameList list = Informedica.GenForm.Library.DomainModel.Lists.UnitNameList.GetNameValueList();
            return this.Direct(list);
        }

        public ActionResult GetSubstanceUnitList()
        {
            Informedica.GenForm.Library.DomainModel.Lists.UnitNameList list = Informedica.GenForm.Library.DomainModel.Lists.UnitNameList.GetNameValueList();
            return this.Direct(list);
        }

        public ActionResult GetBrandList()
        {
            Informedica.GenForm.Library.DomainModel.Lists.BrandNameList list = Informedica.GenForm.Library.DomainModel.Lists.BrandNameList.GetNameValueList();
            return this.Direct(list);
        }

        public ActionResult GetProductSubstances(int productId)
        {
            Product product = Product.GetProduct(productId);
            return this.Direct(product.Substances);
        }

        public ActionResult SaveProductSubstance(ProductSubstance productSubstance)
        {
            bool success = false;
            string msg = string.Empty;
            ProductSubstance substance = null;

            if (Product.Exists(new Product.ExistCriteria(productSubstance.ProductId)))
            {
                Product product = Product.GetProduct(productSubstance.ProductId);
                if (product.HasSubstance(productSubstance.SubstanceName))
                {
                    substance = product.Substances.GetSubstanceByName(productSubstance.SubstanceName);
                }
                else
                {
                    substance = product.Substances.AddNew();

                }

                if (substance != null)
                {
                    Csla.Data.DataMapper.Map(productSubstance, substance, "ProductId");
                    if (product.IsSavable)
                    {
                        try
                        {
                            product = ImportWizard.PrepareProductSave(product);
                            product = (Product)product.Save();
                            substance = product.Substances.GetSubstanceByName(productSubstance.SubstanceName);
                            success = true;
                        }
                        catch (Exception e)
                        {
                            msg = e.Message;
                        }
                    }
                }
            }

            return this.Direct(new
                {
                    success = success,
                    data = substance,
                    msg = msg
                });
        }

        public ActionResult GetRouteList()
        {
            RouteNameList list = RouteNameList.GetNameValueList();
            return this.Direct(list);
        }

        public ActionResult GetProductRoutes(int productId)
        {
            Product product = Product.GetProduct(productId);
            return this.Direct(product.Routes);
        }

        public ActionResult SaveProductRoute(int productId, ProductRoute data)
        {
            ProductRoute route = null;
            bool success = false;
            string msg = string.Empty;

            Product product = Product.GetProduct(productId);
            route = product.Routes.AddRoute(data.RouteId);
            if (!product.IsValid) msg = product.BrokenRulesCollection.ToString();
            try
            {
                product = ImportWizard.PrepareProductSave(product);
                product = (Product)product.Save();
                success = true;
            }
            catch (Exception e)
            {
                msg = msg + ", " + e.Message;
            }

            return this.Direct(new 
                { 
                    success = success, 
                    data = route,  
                    msg = msg
                });
        }

        /// <summary>
        /// Only saves the User parent object, saving the 
        /// children, i.e. needs another method
        /// </summary>
        /// <param name="userdata">New populated User object</param>
        /// <returns>Succes and Saved object</returns>
        [FormHandler]
        public ActionResult SaveProduct(Product productData)
        {
            Product productToSave = null;
            string msg = string.Empty;
            bool success = true;

            // Look whether the user exists. Then get that user, else create a new one
            if (Product.Exists(new Product.ExistCriteria(productData.ProductId)))
            {
                productToSave = Product.GetProduct(productData.ProductId);
                // check for concurrency errors
                if (productToSave.VersionTimeStamp.ToString() != productData.VersionTimeStamp.ToString())
                {
                    productToSave = null;
                    success = false;
                    msg = "Concurrency Error";
                }
            }
            else productToSave = Product.NewProduct();

            // Map the properties of userdata and userTosave, except the Roles collection
            // Roles collection is empty in userdata, needs a seperate method call
            if (productToSave != null)
            {
                Csla.Data.DataMapper.Map(productData, productToSave, "Substances", "Routes", "DefaultName", "KAE");
            }

            try
            {
                productToSave = Informedica.GenForm.Library.DomainModel.Import.ImportWizard.PrepareProductSave(productToSave);
                productToSave = productToSave.Save();
            }
            catch (Exception e)
            {
                msg = e.Message;
                success = false;
            }

            // Return the result
            return this.Direct(
                new
                {
                    success = success,
                    msg = msg,
                    errors = productToSave == null ? string.Empty : msg,
                    data = productToSave == null ? null : productToSave
                });
        }

        public ActionResult GetProductNameList()
        {
            return this.Direct(ProductDisplayNameList.GetNameValueList());
        }

        public ActionResult DeleteProduct(int id)
        {
            bool success = false;
            string msg = string.Empty;

            if (Product.Exists(new Product.ExistCriteria(id)))
            {
                try
                {
                    Product.DeleteProduct(id);
                }
                catch (Exception e)
                {
                    success = false;
                    msg = e.Message;
                }

                success = !Product.Exists(new Product.ExistCriteria(id));
            }

            return this.Direct(new { success = success, responseText = msg });
        }

        public ActionResult DeleteProductSubstance(int productId, int productSubstanceId)
        {
            Product product = null;
            ProductSubstance substance = null;
            bool success = true;
            string msg = string.Empty;

            try
            {
                product = Product.GetProduct(productId);
                substance = (from s in product.Substances
                             where s.ProductSubstanceId == productSubstanceId
                             select s).Single();
                product.Substances.Remove(substance);
                product.Save();
            }
            catch (Exception e)
            {
                success = false;
                msg = e.Message;
            }

            return this.Direct(new { success = success, msg = msg });
        }

        public ActionResult SaveNewProduct(ImportInfo info)
        {
            ImportInfoList list = ImportInfoList.NewProductImportInfoList();
            list.Add(info);

            ImportWizard.ImportProductList(list);
            Product product = Product.GetProductByName(string.Empty);
            return this.Direct(new {});
        }


        public ActionResult CreateNewProduct()
        {
            var services = GenFormServiceProvider.Instance.Resolve<IProductServices>();
            var product = services.CreateProduct();

            var presentation = GetProductPresentation();
            return this.Direct(new { success = true, data = presentation });
        }

        private static IProductPresentation GetProductPresentation()
        {
            return GenFormServiceProvider.Instance.Resolve<IProductPresentation>();
        }

        public ActionResult SaveProduct(object product)
        {
            throw new NotImplementedException();
        }

        public ActionResult UpdateProduct(object product)
        {
            throw new NotImplementedException();
        }
    }
}
