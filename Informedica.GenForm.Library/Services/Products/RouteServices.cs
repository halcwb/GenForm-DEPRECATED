using System.Collections.Generic;
using System.Threading;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Factories;
using Informedica.GenForm.Library.Repositories;

namespace Informedica.GenForm.Library.Services.Products
{
    public class RouteServices : ServicesBase<Route, RouteDto>
    {
        private static RouteServices _instance;
        private static readonly object LockThis = new object();

        private static RouteServices Instance
        {
            get
            {
                if (_instance == null)
                    lock (LockThis)
                    {
                        if (_instance == null)
                        {
                            var instance = new RouteServices();
                            Thread.MemoryBarrier();
                            _instance = instance;
                        }
                    }
                return _instance;
            }
        }


        public static RouteFactory WithDto(RouteDto dto)
        {
            return (RouteFactory)Instance.GetFactory(dto);
        }

        public static IEnumerable<Route> Routes
        {
            get { return Instance.Repository; }
        }

        public static void Delete(Route route)
        {
            route.RemoveAllShapes();
            Instance.Repository.Remove(route);
        }

        public static void ChangeRouteName(Route route, string newName)
        {
            IdentityChanger.ChangeIdentity(route.ChangeName, newName, Instance.Repository);
        }
    }
}