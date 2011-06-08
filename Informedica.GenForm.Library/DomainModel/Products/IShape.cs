using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public interface IShape
    {
        Int32 ShapeId { get; set; }
        String ShapeName { get; set; }
    }
}
