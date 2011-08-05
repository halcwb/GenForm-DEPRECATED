using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Substance = Informedica.GenForm.Database.Substance;

namespace Informedica.GenForm.DataAccess.Repositories.Delegates
{
    public static class SubstanceDelegates
    {
        public static IEnumerable<Substance> Fetch(GenFormDataContext context, Func<Substance, Boolean> selector)
        {
            return context.Substance.Where(selector);
        }

        public static void InsertOnSubmit(GenFormDataContext context, Substance item)
        {
            context.Substance.InsertOnSubmit(item);
        }

        public static void UpdateBo(ISubstance bo, Substance dao)
        {
            bo.SubstanceId = dao.SubstanceId;
        }
    }
}