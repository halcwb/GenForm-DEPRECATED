using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products.Data;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Route: Entity<Guid, RouteDto>
    {
        private HashSet<Shape> _shapes = new HashSet<Shape>(new ShapeComparer());
        private readonly ISet<Product> _products = new HashSet<Product>(new ProductComparer());

        protected Route() : base(new RouteDto()){}

        public Route(RouteDto dto) : base(dto.CloneDto())
        {
            AddShapes();
        }

        private void AddShapes()
        {
            foreach (var shape in Dto.Shapes)
            {
                AddShape(new Shape(shape));
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
            protected set { _shapes = new HashSet<Shape>(value); }
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
        }

        internal protected virtual void AddProduct(Product product)
        {
            _products.Add(product);
        }
    }
}