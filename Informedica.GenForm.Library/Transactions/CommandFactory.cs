using System;
using System.Management.Instrumentation;
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

        public static ISelectCommand<T> CreateSelectCommand<T,TC>(TC argument)
        {
            if (typeof(TC) == typeof(String)) return  (ISelectCommand<T>)ObjectFactory.With(argument).GetInstance<IStringSelectCommand<T>>();
            if (typeof(TC) == typeof(int))
                return (ISelectCommand<T>) ObjectFactory.With(argument).GetInstance<IIntSelectCommand<T>>();

            throw new InstanceNotFoundException();
        }
    }
}
