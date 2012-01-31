using System;
using System.Collections.Generic;
using System.Threading;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Factories;

namespace Informedica.GenForm.Library.Services.Products
{
    public class UnitServices : ServicesBase<Unit, UnitDto>
    {
        private static UnitServices _instance;
        private static readonly object LockThis = new object();

        private static UnitServices Instance
        {
            get
            {
                if (_instance == null)
                    lock (LockThis)
                    {
                        if (_instance == null)
                        {
                            var instance = new UnitServices();
                            Thread.MemoryBarrier();
                            _instance = instance;
                        }
                    }
                return _instance;
            }
        }

        public static UnitFactory WithDto(UnitDto dto)
        {
            return (UnitFactory)Instance.GetFactory(dto);
        }

        public static Unit GetUnit(Guid id)
        {
            return Instance.GetById(id);
        }

        public static IEnumerable<Unit> Units
        {
            get { return Instance.Repository; }
        }

        public static void Delete(Unit unit)
        {
            unit.RemoveFromGroup();
            Instance.Repository.Remove(unit);
        }

        public static void ChangeUnitName(Unit unit, string newName)
        {
            unit.Name = newName;
        }
    }
}