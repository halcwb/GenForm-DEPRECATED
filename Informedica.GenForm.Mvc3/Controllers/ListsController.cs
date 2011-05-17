using System;
using System.Web.Mvc;
using Ext.Direct.Mvc;
using ProductSubstance = Informedica.GenForm.Library.DomainModel.Products.ProductSubstance;
using Route = Informedica.GenForm.Database.Route;
using Shape = Informedica.GenForm.Database.Shape;
using ShapeRoute = Informedica.GenForm.Database.ShapeRoute;

namespace Informedica.GenForm.Mvc2.Controllers
{
    public class ListsController : Controller
    {

        public ActionResult GetRoute(int routeId)
        {
            return this.Direct(new {});
        }

        public ActionResult GetRouteModel()
        {
            return this.Direct(new Route());
        }

        public ActionResult GetShapeRouteModel()
        {
            return this.Direct(new ShapeRoute());
        }

        public ActionResult GetShapeModel()
        {
            return this.Direct(new Shape());
        }

        public ActionResult SaveRoute(Route data)
        {
            bool success = false;
            string msg = string.Empty; 
            Library.DomainModel.Dispense.Route route = null;

            if (Library.DomainModel.Dispense.Route.Exists(data.RouteId))
            {
                route = Library.DomainModel.Dispense.Route.GetRoute(data.RouteId);
                route.TimeStamp = data.VersionTimeStamp.ToArray();
            }
            else
            {
                route = Library.DomainModel.Dispense.Route.NewRoute();
            }

            if (route != null) route.RouteName = data.RouteName;

            try
            {
                route = route.Save();
                success = true;
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            return this.Direct(new
            {
                success = success,
                data = route,
                msg = msg
            });
        }

        public ActionResult GetSubstance(int substanceId)
        {
            return this.Direct(new { });
        }

        public ActionResult SaveSubstance(ProductSubstance substance)
        {
            return this.Direct(new { });
        }

        public ActionResult GetUnit(int unitId)
        {
            return this.Direct(new { });
        }

        public ActionResult SaveUnit(Informedica.GenForm.Library.DomainModel.Lists.Unit route)
        {
            return this.Direct(new { });
        }


    }
}
