using System;
using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Relations;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Route: Entity<Guid, RouteDto>, IRelationPart
    {
        private ISet<Shape> _shapes = new HashedSet<Shape>();
        private ISet<Product> _products = new HashedSet<Product>();

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
            if (_shapes.Contains(shape)) return;
        }

        public virtual String Abbreviation 
        { 
            get { return Dto.Abbreviation ?? (Dto.Abbreviation = CreateAbbreviationFromName()); } 
            set { Dto.Abbreviation = value; } 
        }

        public virtual ISet<Shape> Shapes
        {
            get { return _shapes; }
            protected set { _shapes = value; }
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

        public virtual ISet<Product> Products
        {
            get { return _products; }
            protected set { _products = value;}
        }

        public static Route Create(RouteDto dto)
        {
            return new Route(dto);
        }

        internal protected  virtual void RemoveAllShapes()
        {
            var list = new HashedSet<Shape>(Shapes);
            foreach (var shape in list)
            {
                RemoveShape(shape);
            }
        }

        public virtual void RemoveShape(Shape shape)
        {
            if (_shapes.Contains(shape))
            {
                _shapes.Remove(shape);
                shape.RemoveRoute(this);
            }
        }

        public void AddProduct(Product product)
        {
            if (_products.Contains(product)) return;

            _products.Add(product);
            product.AddRoute(this);
        }

        public void RemoveProduct(Product product)
        {
            if (_products.Contains(product))
            {
                _products.Remove(product);
                product.RemoveRoute(this);
            }
        }
    }
}