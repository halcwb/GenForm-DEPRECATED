using System.Collections;
using System.Collections.Generic;

namespace Informedica.GenForm.Library.DomainModel.Products.Collections
{
    public class ShapeList : IEnumerable<Shape>
    {
        private readonly IList<Shape> _list = new List<Shape>();


        public IEnumerator<Shape> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}