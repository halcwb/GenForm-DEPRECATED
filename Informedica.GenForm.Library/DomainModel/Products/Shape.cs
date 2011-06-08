using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Shape: IShape
    {
        #region Implementation of IShape

        private int _shapeId;

        private string _shapeName;

        public int ShapeId
        {
            get { return _shapeId; }
            set { _shapeId = value; }
        }

        public string ShapeName
        {
            get { return _shapeName; }
            set { _shapeName = value; }
        }

        #endregion
    }
}
