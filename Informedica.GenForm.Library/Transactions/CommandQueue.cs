using System.Collections.Generic;

namespace Informedica.GenForm.Library.Transactions
{
    public class CommandQueue
    {
        private readonly Queue<ICommand> _queue;

        public CommandQueue() {
            _queue = new Queue<ICommand>();}

        public void Enqueue(ICommand command)
        {
            _queue.Enqueue(command);
        }

        public ICommand Dequeue()
        {
            return _queue.Dequeue();
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