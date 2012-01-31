using System;
using System.Collections.Generic;
using System.Threading;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Factories;

namespace Informedica.GenForm.Library.Services.Products
{
    public class SubstanceServices : ServicesBase<Substance, SubstanceDto>
    {
        private static SubstanceServices _instance;
        private static readonly object LockThis = new object();

        private static SubstanceServices Instance
        {
            get
            {
                if (_instance == null)
                    lock (LockThis)
                    {
                        if (_instance == null)
                        {
                            var instance = new SubstanceServices();
                            Thread.MemoryBarrier();
                            _instance = instance;
                        }
                    }
                return _instance;
            }
        }

        public static SubstanceFactory WithDto(SubstanceDto dto)
        {
            return (SubstanceFactory)Instance.GetFactory(dto);
        }

        public static IEnumerable<Substance> Substances
        {
            get { return Instance.Repository; }
        }

        public static void Delete(Substance substance)
        {
            substance.RemoveFromSubstanceGroup();
            Instance.Repository.Remove(substance);
        }

        public static void ChangeSubstanceName(Substance substance, string newName)
        {
            substance.Name = newName;
        }
    }
}