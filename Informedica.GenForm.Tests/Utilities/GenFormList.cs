using System.Collections.Generic;
using System.Linq;

namespace Informedica.GenForm.Tests.Utilities
{
    public class GenFormList
    {
        private readonly IEnumerable<object> _list;

        public GenFormList(IEnumerable<object> list, string type)
        {
            _list = list;
            Type = type;
        }

        public int Count { get { return _list.Count(); } }

        public string Type { get; private set; }
    }
}