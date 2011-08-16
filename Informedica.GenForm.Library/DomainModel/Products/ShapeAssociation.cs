using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.Exceptions;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public static class ShapeAssociation
    {
        public static void AddShape(HashSet<Shape> shapes, Shape shape)
        {
            if (shapes.Contains(shape, new ShapeComparer())) throw new CannotAddItemException<Shape>(shape);
            shapes.Add(shape);
        }
    }
}