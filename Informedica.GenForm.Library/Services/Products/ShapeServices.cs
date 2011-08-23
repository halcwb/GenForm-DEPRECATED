using System;
using System.Collections.Generic;
using System.Threading;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Factories;

namespace Informedica.GenForm.Library.Services.Products
{
    public class ShapeServices : ServicesBase<Shape, Guid, ShapeDto>
    {
        private static ShapeServices _instance;
        private static readonly object LockThis = new object();

        private static ShapeServices Instance
        {
            get
            {
                if (_instance == null)
                    lock (LockThis)
                    {
                        if (_instance == null)
                        {
                            var instance = new ShapeServices();
                            Thread.MemoryBarrier();
                            _instance = instance;
                        }
                    }
                return _instance;
            }
        }

        public static IEnumerable<Shape> Shapes
        {
            get { return Instance.Repository; }
        }

        public static ShapeFactory WithDto(ShapeDto dto)
        {
            return (ShapeFactory)Instance.GetFactory(dto);
        }

        public static void Delete(Shape shape)
        {
            shape.RemoveAllAssociations();
            Instance.Repository.Remove(shape);
        }
    }
}