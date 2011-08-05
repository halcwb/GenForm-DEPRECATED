using Informedica.GenForm.Library.DomainModel.Products;
using Substance = Informedica.GenForm.Database.Substance;

namespace Informedica.GenForm.DataAccess.DataMappers
{
    public class SubstanceMapper : IDataMapper<ISubstance, Substance>
    {
        #region Implementation of IDataMapper<ISubstance,Substance>

        public void MapFromBoToDao(ISubstance bo, Substance dao)
        {
            dao.SubstanceName = bo.SubstanceName;
        }

        public void MapFromDaoToBo(Substance dao, ISubstance bo)
        {
            bo.SubstanceName = dao.SubstanceName;
        }

        #endregion
    }
}