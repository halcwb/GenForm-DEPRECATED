using System;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Transactions.Commands;
using Substance = Informedica.GenForm.Database.Substance;

namespace Informedica.GenForm.DataAccess.Transactions.Commands
{
    public class DeleteSubstanceCommand : CommandBase<ISubstance, Substance>, IDeleteCommand<ISubstance>
    {


        public override void Execute(GenFormDataContext context)
        {
            throw new NotImplementedException();
        }

    }


}
