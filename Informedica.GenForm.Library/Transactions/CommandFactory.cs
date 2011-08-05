using System;
using System.Management.Instrumentation;
using Informedica.GenForm.Library.Transactions.Commands;
using StructureMap;

namespace Informedica.GenForm.Library.Transactions
{
    public class CommandFactory
    {
        public static ICommand CreateInsertCommand<T>(T item)
        {
            return (ICommand)ObjectFactory.With(item).GetInstance<IInsertCommand<T>>();
        }

        public static ICommand CreateSelectCommand<T,TC>(TC argument)
        {
            ISelectCommand<T> command = null;
            command = GetSelectCommand(command, argument);

            if (command == null) throw new InstanceNotFoundException();
            return (ICommand)command;
        }

        public static ICommand CreateSelectCommand<T>()
        {
            return (ICommand)ObjectFactory.GetInstance<ISelectCommand<T>>();
        }

        private static ISelectCommand<T> GetSelectCommand<T, TC>(ISelectCommand<T> command, TC argument)
        {
            if (typeof(TC) == typeof(String)) command = (ISelectCommand<T>)ObjectFactory.With(argument).GetInstance<IStringSelectCommand<T>>();
            if (typeof(TC) == typeof(int)) command = (ISelectCommand<T>) ObjectFactory.With(argument).GetInstance<IIntSelectCommand<T>>();
            return command;
        }

        public static ICommand CreateDeleteCommand<T, TC>(TC argument)
        {
            IDeleteCommand<T> command = null;
            command = GetDeleteCommand(command, argument);

            if (command == null) throw  new InstanceNotFoundException();
            return (ICommand)command;
        }

        private static IDeleteCommand<T> GetDeleteCommand<T, TC>(IDeleteCommand<T> command, TC argument)
        {
            if (typeof(TC) == typeof(String)) command = (IDeleteCommand<T>)ObjectFactory.With(argument).GetInstance<IStringDeleteCommand<T>>();
            if (typeof(TC) == typeof(int)) command = (IDeleteCommand<T>)ObjectFactory.With(argument).GetInstance<IIntDeleteCommand<T>>();
            return command;
        }
    }

}
