using System;
using System.Collections.Generic;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.Transactions;
using StructureMap;

namespace Informedica.GenForm.DataAccess.Transactions.Commands
{
    public abstract class CommandBase<T,TC>: ICommand, IExecutable
    {
        protected Func<TC, Boolean> Selector;
        protected IEnumerable<T> _items;

        protected CommandBase() {}

        protected CommandBase(IEnumerable<T> items)
        {
            _items = items;
        }

        protected CommandBase(Func<TC, Boolean> selector)
        {
            Selector = selector;
        }

        public IEnumerable<T> Items
        {
            get { return _items; }
        }

        protected Repository<T> GetRepository()
        {
            return ObjectFactory.GetInstance<Repository<T>>();
        }

        public abstract void Execute(GenFormDataContext context);
    }
}