using Informedica.GenForm.Library.DomainModel.Products;
using Substance = Informedica.GenForm.Database.Substance;

namespace Informedica.GenForm.DataAccess.Repositories.Delegates
{
    public class GenericDelegates
    {
        public static void UpdateBo(IGeneric bo, Substance dao)
        {
            bo.GenericId = dao.SubstanceId;
        }

    }
}
