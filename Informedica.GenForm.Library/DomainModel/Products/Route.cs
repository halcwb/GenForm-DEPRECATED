using System;
using System.Linq;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.Exceptions;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Route: Entity<Guid, RouteDto>
    {
        private HashSet<Shape> _shapes = new HashSet<Shape>(new ShapeComparer());
        private HashSet<Product> _products = new HashSet<Product>(new ProductComparer());

        protected Route() : base(new RouteDto()){}

        private Route(RouteDto dto) : base(dto.CloneDto())
        {
            AddShapes();
        }

        private void AddShapes()
        {
            foreach (var shape in Dto.Shapes)
            {
                AddShape(Shape.Create(shape));
            }
        }

        public virtual void AddShape(Shape shape)
        {
            shape.AddRoute(this, AddShapeToRoute);
        }

        private void AddShapeToRoute(Shape shape)
        {
            ShapeAssociation.AddShape(_shapes, shape);
        }

        public virtual bool CanNotAddShape(Shape shape)
        {
            throw new NotImplementedException();
        }

        public virtual String Abbreviation 
        { 
            get { return Dto.Abbreviation ?? (Dto.Abbreviation = CreateAbbreviationFromName()); } 
            set { Dto.Abbreviation = value; } 
        }

        public virtual IEnumerable<Shape> Shapes
        {
            get { return _shapes  ?? (_shapes = new HashSet<Shape>()); }
            protected set { _shapes = new HashSet<Shape>(value, new ShapeComparer()); }
        }

        private string CreateAbbreviationFromName()
        {
            int maxLength = Name.Length > 30 ? 30 : Name.Length;
            return Dto.Name.Substring(0, maxLength);
        }

        public override bool IdIsDefault(Guid id)
        {
            return id == Guid.Empty;
        }

        public virtual IEnumerable<Product> Products
        {
            get { return _products; }
            protected set { _products = new HashSet<Product>(value, new ProductComparer());}
        }

        internal protected virtual void AddProduct(Product product)
        {
            if (_products.Contains(product, _products.Comparer)) throw new CannotAddItemException<Product>(product);
            _products.Add(product);
        }

        internal protected virtual void RemoveProduct(Product product)
        {
            if (!_products.Contains(product)) throw new CannotRemoveItemException<Product>(product);
            _products.Remove(product);
        }

        public static Route Create(RouteDto dto)
        {
            return new Route(dto);
        }
    }
}