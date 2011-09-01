using System;
using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products.Interfaces;
using Informedica.GenForm.Library.DomainModel.Validation;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Route: Entity<Route>, IRoute
    {
        public const int AbbreviationLength = 30;
        public new const int NameLength = 50;

        private ISet<Shape> _shapes = new HashedSet<Shape>();
        private ISet<Product> _products = new HashedSet<Product>();
        private RouteDto _dto;

        static Route()
        {
            RegisterValidationRules();
        }

        protected Route()
        {
            _dto = new RouteDto();
        }

        private Route(RouteDto dto)
        {
            ValidateDto(dto);
            AddShapes();
        }

        private void AddShapes()
        {
            foreach (var shape in _dto.Shapes)
            {
                AddShape(Shape.Create(shape));
            }
        }

        public virtual void AddShape(Shape shape)
        {
            if (_shapes.Contains(shape)) return;

            _shapes.Add(shape);
            shape.AddRoute(this);
        }

        public virtual String Abbreviation 
        { 
            get
            {
                if (String.IsNullOrWhiteSpace(_dto.Abbreviation)) SetAbbreviation();
                return _dto.Abbreviation;
            } 
            set { _dto.Abbreviation = value; } 
        }

        public virtual ISet<Shape> Shapes
        {
            get { return _shapes; }
            protected set { _shapes = value; }
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

        internal protected virtual void RemoveAllShapes()
        {
            var list = new HashedSet<Shape>(Shapes);
            foreach (var shape in list)
            {
                RemoveShape(shape);
            }
        }

        internal protected  virtual void RemoveAllProducts()
        {
            var list = new HashedSet<Product>(Products);
            foreach (var product in list)
            {
                RemoveProduct(product);
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

        public virtual void AddProduct(Product product)
        {
            if (_products.Contains(product)) return;

            _products.Add(product);
            product.AddRoute(this);
        }

        public virtual void RemoveProduct(Product product)
        {
            if (_products.Contains(product))
            {
                _products.Remove(product);
                product.RemoveRoute(this);
            }
        }

        public override Guid Id { get { return _dto.Id; } protected set { _dto.Id = value; } }

        public override string Name
        {
            get { return _dto.Name ?? String.Empty; } 
            protected set { SetRouteName(value); }
        }

        private static void RegisterValidationRules()
        {
            ValidationRulesManager.RegisterRule<RouteDto>(x => !String.IsNullOrWhiteSpace(x.Name), "Route has to have a name");
            ValidationRulesManager.RegisterRule<RouteDto>(x => !String.IsNullOrWhiteSpace(x.Abbreviation));
        }

        protected override void SetDto<TDto>(TDto dto)
        {
            var dataTransferObject = dto as DataTransferObject<RouteDto>;
            if (dataTransferObject != null) _dto = dataTransferObject.CloneDto();
        }

        private void SetRouteName(string value)
        {
            if (String.IsNullOrWhiteSpace(value) || Name.Equals(value)) return;

            var length = value.Length > NameLength ? NameLength : value.Length;
            _dto.Name = value.Substring(0, length).Trim().ToLower();

            SetAbbreviation();
        }

        private void SetAbbreviation()
        {
            var length = _dto.Name.Length > AbbreviationLength ? AbbreviationLength : _dto.Name.Length;
            if (String.IsNullOrEmpty(_dto.Abbreviation)) _dto.Abbreviation = _dto.Name.Substring(0, length);
        }

        internal protected virtual void ChangeName(string name)
        {
            Name = name;
        }

    }
}