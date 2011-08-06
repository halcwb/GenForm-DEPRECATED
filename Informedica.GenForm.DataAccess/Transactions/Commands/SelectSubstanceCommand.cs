using System;
using System.Collections.Generic;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Transactions.Commands;
using StructureMap;
using Substance = Informedica.GenForm.Database.Substance;

namespace Informedica.GenForm.DataAccess.Transactions.Commands
{
    public class SelectSubstanceCommand: CommandBase<ISubstance,Substance>, ISelectCommand<ISubstance>
    {
        [DefaultConstructor]
        public SelectSubstanceCommand(): this(s => true) {}

        public SelectSubstanceCommand(String name): this(s => s.SubstanceName == name) {}

        public SelectSubstanceCommand(Int32 id) : this(s => s.SubstanceId == id) {}

        public SelectSubstanceCommand(Func<Substance,Boolean> selector): base(selector) {}

        public override void Execute(GenFormDataContext context)
        {
            _items = GetRepository().Fetch(context, Selector);
        }

        public IEnumerable<ISubstance> Result
        {
            get { return Items; }
        }
    }

}