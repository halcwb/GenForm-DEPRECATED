using System.Collections.Generic;
using System.Threading;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Factories;

namespace Informedica.GenForm.Library.Services.Products
{
    public class BrandServices : ServicesBase<Brand, BrandDto>
    {
        private static BrandServices _instance;
        private static readonly object LockThis = new object();

        private static BrandServices Instance
        {
            get
            {
                if (_instance == null)
                    lock (LockThis)
                    {
                        if (_instance == null)
                        {
                            var instance = new BrandServices();
                            Thread.MemoryBarrier();
                            _instance = instance;
                        }
                    }
                return _instance;
            }
        }

        public static BrandFactory WithDto(BrandDto dto)
        {
            return (BrandFactory) Instance.GetFactory(dto);
        }

        public static IEnumerable<Brand> Brands
        {
            get { return Instance.Repository; }
        }

        public static void Delete(Brand brand)
        {
            Instance.Repository.Remove(brand);
        }

        public static void ChangeBrandName(Brand brand, string name)
        {
            IdentityChanger.ChangeIdentity(brand.ChangeName, name, Instance.Repository);
        }
    }
}