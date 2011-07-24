using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.Repositories;
using Informedica.GenForm.Library.Services;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class SubstanceRepository: ISubstanceRepository
    {
        #region Implementation of IRepository<ISubstance>

        public IEnumerable<ISubstance> Fetch(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ISubstance> Fetch(string name)
        {
            throw new NotImplementedException();
        }

        public void Insert(ISubstance item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(ISubstance item)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}