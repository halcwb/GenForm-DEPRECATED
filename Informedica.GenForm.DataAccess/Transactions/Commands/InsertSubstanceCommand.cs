using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Transactions.Commands;
using StructureMap;
using Substance = Informedica.GenForm.Database.Substance;

namespace Informedica.GenForm.DataAccess.Transactions.Commands
{
    public class InsertSubstanceCommand: CommandBase<ISubstance, Substance>, IInsertCommand<ISubstance>
    {
        [DefaultConstructor]
        public InsertSubstanceCommand(ISubstance substance): base(new List<ISubstance>{substance}) {}

        public override void Execute(GenFormDataContext context)
        {
            GetRepository().Insert(context, Item);
        }

        public ISubstance Item
        {
            get { return Items.First(); }
        }
    }

}