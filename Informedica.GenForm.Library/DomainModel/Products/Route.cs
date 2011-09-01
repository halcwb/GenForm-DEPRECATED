using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products.Collections;
using Informedica.GenForm.Library.DomainModel.Products.Interfaces;
using Informedica.GenForm.Library.DomainModel.Validation;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Route: Entity<Route>, IRoute
    {
        public const int AbbreviationLength = 30;
        public new const int NameLength = 50;

        #region Private

        private RouteDto _dto;
        private ShapeSet<Route> _shapes;
        private ProductSet<Route> _products;

        #endregion

        #region Construction

        static Route()
        {
            RegisterValidationRules();
        }

        protected Route()
        {
            _dto = new RouteDto();
            InitializeCollections();
        }

        private Route(RouteDto dto)
        {
            ValidateDto(dto);
            InitializeCollections();
            AddShapes();
        }

        private void InitializeCollections()
        {
            _shapes = new ShapeSet<Route>(this);
            _products = new ProductSet<Route>(this);
        }

        private void AddShapes()
        {
            foreach (var shape in _dto.Shapes)
            {
                AddShape(Shape.Create(shape));
            }
        }

        #endregion

        #region Business

        public override Guid Id { get { return _dto.Id; } protected set { _dto.Id = value; } }

        public override string Name
        {
            get { return _dto.Name ?? String.Empty; }
            protected set { SetRouteName(value); }
        }

        private void SetRouteName(string value)
        {
            if (String.IsNullOrWhiteSpace(value) || Name.Equals(value)) return;

            var length = value.Length > NameLength ? NameLength : value.Length;
            _dto.Name = value.Substring(0, length).Trim().ToLower();

            SetAbbreviation();
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

        private void SetAbbreviation()
        {
            var length = _dto.Name.Length > AbbreviationLength ? AbbreviationLength : _dto.Name.Length;
            if (String.IsNullOrEmpty(_dto.Abbreviation)) _dto.Abbreviation = _dto.Name.Substring(0, length);
        }

        internal protected virtual void ChangeName(string name)
        {
            Name = name;
        }

        public virtual IEnumerable<IShape> Shapes
        {
            get { return _shapes; }
        }

        public virtual Iesi.Collections.Generic.ISet<Shape> ShapeSet
        {
            get { return _shapes.GetEntitySet(); }
            protected set { _shapes = new ShapeSet<Route>(value, this); }
        }

        public virtual void AddShape(IShape shape)
        {
            _shapes.Add((Shape)shape, ((Shape)shape).AddRoute);
        }

        public virtual void RemoveShape(IShape shape)
        {
            _shapes.Remove((Shape)shape, ((Shape)shape).RemoveRoute);
        }

        public virtual IEnumerable<IProduct> Products
        {
            get { return _products; }
        }

        public virtual Iesi.Collections.Generic.ISet<Product> ProductSet
        {
            get { return _products.GetEntitySet(); }
            protected set { _products = new ProductSet<Route>(value, this); }
        }

        public virtual void AddProduct(IProduct product)
        {
            _products.Add((Product)product, ((Product)product).AddRoute);
        }

        public virtual void RemoveProduct(IProduct product)
        {
            _products.Remove((Product)product, ((Product)product).RemoveRoute);
        }

        internal protected virtual void RemoveAllShapes()
        {
            var list = new HashedSet<Shape>(ShapeSet);
            foreach (var shape in list)
            {
                RemoveShape(shape);
            }
        }

        internal protected virtual void RemoveAllProducts()
        {
            var list = new HashedSet<Product>(ProductSet);
            foreach (var product in list)
            {
                RemoveProduct(product);
            }
        }

        #endregion

        #region Factory
        
        public static Route Create(RouteDto dto)
        {
            return new Route(dto);
        }

        #endregion

        #region Validation

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

        #endregion

    }
}