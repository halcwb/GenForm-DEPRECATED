using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Products.Collections;

namespace Informedica.GenForm.Tests.Utilities
{
    public class GenFormLists
    {
        private readonly IList<GenFormList> _list = new List<GenFormList>();

        public GenFormLists()
        {
            Initialize();
        }

        private void Initialize()
        {
            _list.Add(new GenFormList(new ShapeList(), "shapes"));
        }

        public GenFormList GetListByType(string type)
        {
            return _list.Single(x => x.Type == type);
        }
    }
}