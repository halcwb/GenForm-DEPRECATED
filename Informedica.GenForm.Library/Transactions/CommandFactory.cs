using System;
using Informedica.GenForm.Library.Transactions.Commands;
using StructureMap;

namespace Informedica.GenForm.Library.Transactions
{
    public class CommandFactory
    {
        public static IInsertCommand<T> CreateInsertCommand<T>(T item)
        {
            return ObjectFactory.With(item).GetInstance<IInsertCommand<T>>();
        }

        public static ISelectCommand<T> CreateSelectCommand<T>(Int32 id)
        {
            return ObjectFactory.With(id).GetInstance<ISelectCommand<T>>();
        }
    }
}
