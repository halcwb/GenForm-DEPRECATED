using System.Collections.Generic;

namespace Informedica.GenForm.Library.Transactions
{
    public class CommandList
    {
        private readonly IList<ICommand> _list;
        private readonly Queue<ICommand> _queue;

        public CommandList() { _list = new List<ICommand>(); _queue = new Queue<ICommand>();}

        public void Add(ICommand command)
        {
            _queue.Enqueue(command);
        }

        public IEnumerable<ICommand> Commands
        {
            get { return _queue; }
        }

        public ICommand Peek()
        {
            return _queue.Peek();
        }
    }
}