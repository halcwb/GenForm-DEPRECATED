using System.Collections.Generic;
using System.Threading;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Factories;

namespace Informedica.GenForm.Library.Services.Products
{
    public class PackageServices : ServicesBase<Package, PackageDto>
    {
        private static PackageServices _instance;
        private static readonly object LockThis = new object();

        private static PackageServices Instance
        {
            get
            {
                if (_instance == null)
                    lock (LockThis)
                    {
                        if (_instance == null)
                        {
                            var instance = new PackageServices();
                            Thread.MemoryBarrier();
                            _instance = instance;
                        }
                    }
                return _instance;
            }
        }

        public static IEnumerable<Package> Packages
        {
            get { return Instance.Repository; }
        }


        public static PackageFactory WithDto(PackageDto dto)
        {
            return (PackageFactory)Instance.GetFactory(dto);
        }

        public static void Delete(Package package)
        {
            package.RemoveAllShapes();
            Instance.Repository.Remove(package);
        }
    }
}