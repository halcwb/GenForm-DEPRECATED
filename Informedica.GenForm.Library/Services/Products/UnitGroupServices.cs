using System.Collections.Generic;
using System.Threading;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Factories;

namespace Informedica.GenForm.Library.Services.Products
{
    public class UnitGroupServices : ServicesBase<UnitGroup, UnitGroupDto>
    {
        private static UnitGroupServices _instance;
        private static readonly object LockThis = new object();

        private static UnitGroupServices Instance
        {
            get
            {
                if (_instance == null)
                    lock (LockThis)
                    {
                        if (_instance == null)
                        {
                            var instance = new UnitGroupServices();
                            Thread.MemoryBarrier();
                            _instance = instance;
                        }
                    }
                return _instance;
            }
        }

        public static IEnumerable<UnitGroup> UnitGroups
        {
            get { return Instance.Repository; }
        }


        public static UnitGroupFactory WithDto(UnitGroupDto dto)
        {
            return (UnitGroupFactory)Instance.GetFactory(dto);
        }
    }
}