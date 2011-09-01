using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products.Collections;
using Informedica.GenForm.Library.DomainModel.Products.Interfaces;
using Informedica.GenForm.Library.DomainModel.Validation;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Package: Entity<Package>, IPackage
    {
        #region Private

        private PackageDto _dto;
        private ShapeSet<Package> _shapes;
        private ProductSet<Package> _products;

        #endregion

        #region Construction

        static Package()
        {
            RegisterValidationRules();
        }

        protected Package()
        {
            _dto = new PackageDto();
            InitializeCollections();
        }

        private Package(PackageDto dto)
        {
            ValidateDto(dto);
            InitializeCollections();
        }

        private void InitializeCollections()
        {
            _shapes = new ShapeSet<Package>(this);
            _products = new ProductSet<Package>(this);
        }

        #endregion

        #region Business

        public override Guid Id { get { return _dto.Id; } protected set { _dto.Id = value; } }

        public override string Name { get { return _dto.Name; } protected set { _dto.Name = value; } }

        public virtual String Abbreviation
        {
            get { return _dto.Abbreviation ?? GetAbbreviatedName(); }
            set { _dto.Abbreviation = value; }
        }

        private string GetAbbreviatedName()
        {
            int maxlength = Name.Length > 30 ? 30 : Name.Length;
            return _dto.Name.Substring(0, maxlength);
        }

        public virtual IEnumerable<IShape> Shapes
        {
            get { return _shapes; }
        }

        public virtual Iesi.Collections.Generic.ISet<Shape> ShapeSet
        {
            get { return _shapes.GetEntitySet(); }
            protected set { _shapes = new ShapeSet<Package>(value, this); }
        }

        public virtual bool ContainsShape(IShape shape)
        {
            return _shapes.Contains((Shape)shape);
        }

        public virtual void AddShape(IShape shape)
        {
            _shapes.Add((Shape)shape, ((Shape)shape).AddPackage);
        }

        public virtual void RemoveShape(IShape shape)
        {
            _shapes.Remove((Shape)shape, ((Shape)shape).RemovePackage);
        }

        public virtual IEnumerable<IProduct> Products
        {
            get { return _products; }
        }

        public virtual Iesi.Collections.Generic.ISet<Product> ProductSet
        {
            get { return _products.GetEntitySet(); }
            protected set { _products = new ProductSet<Package>(value, this); }
        }

        public virtual void AddProduct(IProduct product)
        {
            _products.Add((Product)product, ((Product)product).SetPackage);
        }

        internal protected virtual void RemoveProduct(Product product)
        {
            _products.Remove(product);
        }

        internal protected virtual void RemoveAllShapes()
        {
            var list = new HashedSet<Shape>(ShapeSet);
            foreach (var shape in list)
            {
                RemoveShape(shape);
            }
        }

        #endregion

        #region Factory

        public static Package Create(PackageDto dto)
        {
            return new Package(dto);
        }

        #endregion

        #region Validation

        private static void RegisterValidationRules()
        {
            ValidationRulesManager.RegisterRule<PackageDto>(x => !String.IsNullOrWhiteSpace(x.Name));
        }

        protected override void SetDto<TDto>(TDto dto)
        {
            var dataTransferObject = dto as DataTransferObject<PackageDto>;
            if (dataTransferObject != null) _dto = dataTransferObject.CloneDto();
        }

        #endregion
    }
}
