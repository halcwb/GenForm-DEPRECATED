using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;
using Informedica.GenForm.DomainModel.Interfaces;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products.Collections;
using Informedica.GenForm.Library.DomainModel.Validation;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Route: Entity<Route>, IRoute
    {
        public const int AbbreviationLength = 30;
        public new const int NameLength = 50;

        #region Private

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
            InitializeCollections();
        }


        private void InitializeCollections()
        {
            _shapes = new ShapeSet<Route>(this);
            _products = new ProductSet<Route>(this);
        }

        #endregion

        #region Business

        public virtual String Abbreviation { set; get; }

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
            var route = new Route
                       {
                           Name = dto.Name,
                           Abbreviation = dto.Abbreviation
                       };
            Validate(route);
            return route;
        }

        #endregion

        #region Validation

        private static void RegisterValidationRules()
        {
            ValidationRulesManager.RegisterRule<Route>(x => !String.IsNullOrWhiteSpace(x.Name), "Route has to have a name");
            ValidationRulesManager.RegisterRule<Route>(x => !String.IsNullOrWhiteSpace(x.Abbreviation), "Route has to have a abbreviation");
        }

        #endregion

        #region Overrides of Entity<Route,Guid>

        public override bool IsIdentical(Route entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}