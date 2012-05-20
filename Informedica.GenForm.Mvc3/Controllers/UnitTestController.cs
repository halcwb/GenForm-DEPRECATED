using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ext.Direct.Mvc;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Services.Environments;
using Informedica.GenForm.Services.UserLogin;
using Informedica.GenForm.TestFixtures.Fixtures;
using Newtonsoft.Json.Linq;

namespace Informedica.GenForm.Mvc3.Controllers
{

    public class UnitTestController : Controller
    {
        //
        // GET: /Tests/

        public ActionResult Index()
        {
            ViewBag.Title = "GenForm Tests";
            return View();
        }

        #region LoginController

        //public ActionResult GetEnvironments()
        //{
        //    var names = new List<string> { "GenFormTest", "GenFormAcceptatie", "GenFormProductie" }; //ToDo: EnvironmentServices.GetDatabases();
        //    IList<object> list = new List<object>();
        //    foreach (var name in names)
        //    {
        //        list.Add(new { Environment = name });
        //    }
        //    return this.Direct(list);
        //}

        public ActionResult RegisterEnvironment(EnvironmentDto environment)
        {
            var success = !String.IsNullOrWhiteSpace(environment.Name) &&
                          !String.IsNullOrWhiteSpace(environment.Database) && environment.Database.StartsWith("Data Source=") &&
                          (String.IsNullOrWhiteSpace(environment.LogPath) || environment.LogPath.StartsWith("c:\\")) &&
                          (String.IsNullOrWhiteSpace(environment.ExportPath) || environment.ExportPath.StartsWith("c:\\"));

            return this.Direct(new { success, data = environment });
        }

        public ActionResult SetEnvironment(String environment)
        {
            if (HttpContext.Session != null && !String.IsNullOrWhiteSpace(environment)) HttpContext.Session.Add("environment", environment);

            return this.Direct(new { success = !String.IsNullOrWhiteSpace(environment) });
        }

        public ActionResult Login(UserLoginDto login)
        {
            var success = login.UserName == "Admin" &&
                          login.Password == "Admin" &&
                          !string.IsNullOrWhiteSpace(login.Environment);

            return this.Direct(new { success });
        }

        public ActionResult IsLoggedIn()
        {
            return this.Direct(new { success = true });
        }

        public ActionResult Logout(String username)
        {
            return this.Direct(new { success = (username == "Admin") });
        }

        public ActionResult GetLoggedInUser()
        {
            return this.Direct(new { user = "Admin" });
        }

        #endregion

        #region ProductController

        public ActionResult GetGenericNames()
        {
            var generics = new[]
            {
                new { Id = Guid.NewGuid(), Name = "paracetamol" },
                new { Id = Guid.NewGuid(), Name = "dopamine" },
                new { Id = Guid.NewGuid(), Name = "midazolam" },
                new { Id = Guid.NewGuid(), Name = "TPN" },
                new { Id = Guid.NewGuid(), Name = "morfine" },
                new { Id = Guid.NewGuid(), Name = "penicilline" }
            };
            return this.Direct(new { success = true, data = generics });
        }

        public ActionResult GetSubstanceNames()
        {
            var substances = new[]
            {
                new { Id = Guid.NewGuid(), Name = "paracetamol" },
                new { Id = Guid.NewGuid(), Name = "dopamine" },
                new { Id = Guid.NewGuid(), Name = "midazolam" },
                new { Id = Guid.NewGuid(), Name = "morfine" },
                new { Id = Guid.NewGuid(), Name = "penicilline" }
            };
            return this.Direct(new { success = true, data = substances });
        }

        public ActionResult GetBrandNames()
        {
            var brands = new[]
            {
                new { Id = Guid.NewGuid(), Name = "Augmentin" },
                new { Id = Guid.NewGuid(), Name = "Zithromax" },
                new { Id = Guid.NewGuid(), Name = "Dynatra" },
                new { Id = Guid.NewGuid(), Name = "Esmeron" },
                new { Id = Guid.NewGuid(), Name = "Perfalgan" }
            };
            return this.Direct(new { success = true, data = brands });
        }

        public ActionResult GetShapeNames()
        {
            var shapes = new[]
            {
                new { Id = Guid.NewGuid(), Name = "tablet" },
                new { Id = Guid.NewGuid(), Name = "infusievloeistof" },
                new { Id = Guid.NewGuid(), Name = "zetpil" },
                new { Id = Guid.NewGuid(), Name = "zalf" },
                new { Id = Guid.NewGuid(), Name = "inhalatiepoeder" }
            };
            return this.Direct(new { success = true, data = shapes });
        }

        public ActionResult GetPackageNames()
        {
            var packages = new[]
            {
                new { Id = Guid.NewGuid(), Name = "tablet" },
                new { Id = Guid.NewGuid(), Name = "fles" },
                new { Id = Guid.NewGuid(), Name = "zetpil" },
                new { Id = Guid.NewGuid(), Name = "tube" },
                new { Id = Guid.NewGuid(), Name = "ampul" }
            };
            return this.Direct(new { success = true, data = packages });
        }

        public ActionResult GetUnitNames()
        {
            var units = new[]
            {
                new { Id = Guid.NewGuid(), Name = "stuk"},
                new { Id = Guid.NewGuid(), Name = "mL"}
            };
            return this.Direct(new { success = true, data = units });
        }

        public ActionResult GetRouteNames()
        {
            var routes = new[]
            {
                new { Id = Guid.NewGuid(), Name = "iv"},
                new { Id = Guid.NewGuid(), Name = "or"}
            };
            return this.Direct(new { success = true, data = routes });
        }

        public ActionResult SaveProduct(ProductDto product)
        {
            var success = !String.IsNullOrWhiteSpace(product.GenericName);
            var message = String.Empty;
            product.DisplayName = "dopamine Dynatra infusievloeistof 5 mL ampul";

            return this.Direct(new { success, data = product, message });
        }

        public ActionResult DeleteProduct(JObject id)
        {
            var guid = Guid.Parse(id.Value<string>("id"));
            var success = guid.CompareTo(Guid.Empty) != 0;
            return this.Direct(new { success });
        }

        public ActionResult GetProduct(JObject id)
        {
            if (Guid.Parse(id.Value<string>("id")).CompareTo(Guid.Empty) == 0)
                return this.Direct(new { success = false });

            var product = ProductTestFixtures.GetDopamineDto();
            return this.Direct(new { success = true, data = product });
        }

        public ActionResult GetProducts()
        {
            return this.Direct(new[]
            {                    
                ProductTestFixtures.GetProductDtoListWithThreeItems()
            });
        }

        #endregion

        #region ShapeController

        public ActionResult GetShapes()
        {
            var shapes = new List<ShapeDto> { ShapeTestFixtures.GetValidDtoWithUnitGroups() };
            return this.Direct(new { success = true, data = shapes });
        }

        public ActionResult GetShape(JObject id)
        {
            if (CheckIfIdIsEmpty(id, "id"))
                return this.Direct(new { success = false });

            var shape = ShapeTestFixtures.GetValidDtoWithUnitGroups();
            return this.Direct(new { success = true, result = shape });
        }

        public ActionResult SaveShape(ShapeDto dto)
        {
            if (String.IsNullOrWhiteSpace(dto.Name))
                return this.Direct(new { success = false });

            if (dto.Id == Guid.Empty.ToString()) dto.Id = new Guid().ToString();
            return this.Direct(new { success = true, data = dto });
        }

        public ActionResult DeleteShape(JObject id)
        {
            if (CheckIfIdIsEmpty(id, "id"))
                return this.Direct(new { success = false });

            return this.Direct(new { success = true });
        }

        #endregion

        #region PackageController

        public ActionResult GetPackages()
        {
            var packages = new List<PackageDto> { PackageTestFixtures.GetDtoWithTwoShapes() };
            return this.Direct(new { success = true, data = packages });
        }

        public ActionResult GetPackage(JObject id)
        {
            if (CheckIfIdIsEmpty(id, "id"))
                return this.Direct(new { success = false });

            var shape = PackageTestFixtures.CreatePackageAmpul();
            return this.Direct(new { success = true, result = shape });
        }

        public ActionResult SavePackage(PackageDto dto)
        {
            if (String.IsNullOrWhiteSpace(dto.Name))
                return this.Direct(new { success = false });

            if (dto.Id == Guid.Empty.ToString()) dto.Id = new Guid().ToString();
            return this.Direct(new { success = true, data = dto });
        }

        public ActionResult DeletePackage(JObject id)
        {
            if (CheckIfIdIsEmpty(id, "id"))
                return this.Direct(new { success = false });

            return this.Direct(new { success = true });
        }

        #endregion

        #region UnitController

        public ActionResult GetUnits()
        {
            var units = new List<UnitDto>
            {
                UnitTestFixtures.GetTestUnitMililiter(), 
                UnitTestFixtures.GetTestUnitMilligram()
            };

            return this.Direct(new { success = true, data = units });
        }

        public ActionResult GetUnit(JObject id)
        {
            if (CheckIfIdIsEmpty(id, "id"))
                return this.Direct(new { success = false });

            var shape = UnitTestFixtures.CreateUnitMililiter();
            return this.Direct(new { success = true, result = shape });
        }

        public ActionResult SaveUnit(UnitDto dto)
        {
            if (String.IsNullOrWhiteSpace(dto.Name))
                return this.Direct(new { success = false });

            if (dto.Id == Guid.Empty.ToString()) dto.Id = new Guid().ToString();
            return this.Direct(new { success = true, data = dto });
        }

        public ActionResult DeleteUnit(JObject id)
        {
            if (CheckIfIdIsEmpty(id, "id"))
                return this.Direct(new { success = false });

            return this.Direct(new { success = true });
        }

        public ActionResult GetUnitGroupNames()
        {
            var unitgroups = new[]
            {
                new { Id = Guid.NewGuid(), Name = "verpakking"},
                new { Id = Guid.NewGuid(), Name = "volume"},
                new { Id = Guid.NewGuid(), Name = "massa"}
            };
            return this.Direct(new { success = true, data = unitgroups });
        }

        #endregion

        #region RouteController

        public ActionResult GetRoutes()
        {
            var routes = new List<RouteDto> { RouteTestFixtures.GetRouteWithShape() };
            return this.Direct(new { success = true, data = routes });
        }

        public ActionResult GetRoute(JObject id)
        {
            if (CheckIfIdIsEmpty(id, "id"))
                return this.Direct(new { success = false });

            var shape = RouteTestFixtures.GetRouteWithShape();
            return this.Direct(new { success = true, result = shape });
        }

        public ActionResult SaveRoute(RouteDto dto)
        {
            if (String.IsNullOrWhiteSpace(dto.Name))
                return this.Direct(new { success = false });

            if (dto.Id == Guid.Empty.ToString()) dto.Id = new Guid().ToString();
            return this.Direct(new { success = true, data = dto });
        }

        public ActionResult DeleteRoute(JObject id)
        {
            if (CheckIfIdIsEmpty(id, "id"))
                return this.Direct(new { success = false });

            return this.Direct(new { success = true });
        }

        #endregion

        #region SubstanceController

        public ActionResult GetSubstances()
        {
            var substances = SubstanceTestFixtures.GetSubstanceDtoListWithThreeItems();
            return this.Direct(new { success = true, data = substances });
        }

        public ActionResult GetSubstance(JObject id)
        {
            if (CheckIfIdIsEmpty(id, "id"))
                return this.Direct(new { success = false });

            var subst = SubstanceTestFixtures.CreateSubstanceWithoutGroup();
            return this.Direct(new { success = true, data = subst });
        }

        public ActionResult SaveSubstance(SubstanceDto dto)
        {
            if (String.IsNullOrWhiteSpace(dto.Name))
                return this.Direct(new { success = false });

            if (dto.Id == Guid.Empty.ToString()) dto.Id = new Guid().ToString();
            return this.Direct(new { success = true, data = dto });
        }

        public ActionResult DeleteSubstance(JObject id)
        {
            if (CheckIfIdIsEmpty(id, "id"))
                return this.Direct(new { success = false });

            return this.Direct(new { success = true });
        }

        #endregion

        #region GenericNameController

        public ActionResult AddNewGenericName(GenericNameDto dto)
        {
            return this.Direct(new
            {
                success = !String.IsNullOrWhiteSpace(dto.Name),
                data = dto
            });
        }

        #endregion

        #region BrandNameController

        public ActionResult AddNewBrandName(BrandDto dto)
        {
            return this.Direct(new
            {
                success = !String.IsNullOrWhiteSpace(dto.Name),
                data = dto
            });
        }

        #endregion

        #region EnvironmentController

        public ActionResult GetEnvironments()
        {
            return this.Direct(new
                                   {
                                       success = true,
                                       data = new[]
                                                  {
                                                      new
                                                          {
                                                              Id = '1',
                                                              Name = "GenFormTest",
                                                              Connection = "TestConnection",
                                                              LogPath = "c/test/logpath",
                                                              ExportPath = "c/test/exportpath"
                                                          },
                                                      new
                                                          {
                                                              Id = '2',
                                                              Name = "GenFormAcceptance",
                                                              Connection = "TestConnection",
                                                              LogPath = "c/test/logpath",
                                                              ExportPath = "c/test/exportpath"
                                                          },
                                                      new
                                                          {
                                                              Id = '3',
                                                              Name = "GenFormProduction",
                                                              Connection = "TestConnection",
                                                              LogPath = "c/test/logpath",
                                                              ExportPath = "c/test/exportpath"
                                                          }
                                                  }
                                   });
        }

        #endregion

        private static bool CheckIfIdIsEmpty(JToken id, string idField)
        {
            return Guid.Parse(id.Value<string>(idField)).CompareTo(Guid.Empty) == 0;
        }
    }

}
